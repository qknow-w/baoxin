var YXQCore = {
    Form: {},
    Dialog: {}, 
    Toggle: {},
    Cookie: {}
};

YXQCore.Toggle = {
    Tog: function (div) {
        $("#" + div).toggle();
    }
};
YXQCore.Cookie = {
    SetCookie: function (name, value,day) {
        //var today = new Date();
        //var expire = new Date();
        //expire.setTime(today.getTime() + 3600000 * 24*day);
        //document.cookie = escape(name + ";" + value);

        var temp = name + "=" + escape(value);
        if (day)
        {
            temp += ";expires=" + day.toGMTString();
        }
        document.cookie = temp;

    },
    GetCookie: function (v) {
        //v = escape(v);
        //var theCookie = "" + document.cookie;

        //var ind = theCookie.indexOf(v); 
        //if (ind == -1)
        //{ return ""; }
        //return unescape(theCookie.substring(7, 11));
        var arr, reg = new RegExp("(^|)" + name + "=([^;]*)(;|$)");
        if (arr = document.cookie.match(reg)) {
            return unescape(arr[2]);
        }
        return null;
    },
    DelCookie: function (name) {
        var val = YXQCore.Cookie.GetCookie(name);
        if (val)
        {
            var expiress = new Date();
            expiress.setTime(expiress.getTime() - 10000);
            YXQCore.Cookie.SetCookie(name,val,expiress);
        }
    }
};

YXQCore.Form = {

    PostPageSync: function (url, div, data) {
        if (data == undefined)
            data = {};
        $.ajax({
            type: "Post",
            url: url,
            data: data,
            cache: false,
            async: false,
            beforeSend: function (xhr) {
                $(div).html("<img src ='Images/loading.gif' alt='loading'  />");
            },
            success: function (response) {
                if (response.indexOf("HasLogOut") < 0) {
                    $(div).html(response);
                }
                else
                    alert("请重新登录！");
            },
            error: function (xml, status) {
                $(div).html(xml.responseText);
            }
        });
    },
    PostData: function (url, data, afterRender, onError) {
        if (data == undefined)
            data = {};
        $.ajax({
            type: "get",
            url: url,
            data: data,
            cache: false,
            beforeSend: function (xhr) {
                YXQCore.Form.ShowMaskLayer();
            },
            success: function (response) {

                if (afterRender != undefined) {
                    afterRender(response);
                }
                YXQCore.Form.CloseMaskLayer();

            },
            error: function (xml, status) {
               
                //alert(xml.responseText + "status=" + status);
                YXQCore.Form.CloseMaskLayer();
            }

        });
    },

    FileUpload: function (url,  data, afterRender) {
        $("#uploadify").uploadify({
            uploader: '../Infrastructure/uploadify.swf',
            script: url,
            cancelImg: '../Infrastructure/cancel.png',
            queueID: 'fileQueue',
            auto: false,
            multi: false,
            scriptData: data,
            onComplete: function (event, queueId, fileObj, response, data) {
                if (afterRender != undefined) {
                    afterRender(response.toString());
                }

            },
            onAllComplete: function (data) {
                if (afterRender != undefined) {
                    afterRender(data.toString());
                }
            },
            onError: function (event, queueId, fileObj, errorObj) {
                alert(errorObj.info);
            }
        });

    },

    ShowMaskLayer: function () {
        $("#Masklayer").css({ position: "absolute",
            height: Math.max(document.body.scrollHeight, document.documentElement.scrollHeight) + "px",
            width: Math.max(document.body.scrollWidth, document.documentElement.scrollWidth) + "px",
            filter: "alpha(opacity=50)",
            opacity: "0.5",
            top: "0",
            left: "0",
            index: "9000",
            background: "#CCCCCC",
            display: "block"
        });
        $("#Masklayer").show();
    },
    CloseMaskLayer: function () {
        $("#Masklayer").hide();
    },

    PostPage: function (url, div, data, afterRender) {
        if (data == undefined)
            data = {};
        $.ajax({
            type: "Post",
            url: url,
            data: data,
            cache: false,
            beforeSend: function (xhr) {
                $(div).html("<img src ='Images/loading.gif' alt='loading'  />");
                //增加遮罩层显示
                YXQCore.Form.ShowMaskLayer();
                //alert("ff");
            },
            success: function (response) {

                $(div).html(response);
                if (afterRender != undefined) {
                    afterRender(response);
                }
                //关闭遮罩层
                YXQCore.Form.CloseMaskLayer();

            },
            error: function (xml, status) {
                $(div).html(xml.responseText);
                YXQCore.Form.CloseMaskLayer();
            }
        });
    },
    GetDivFormData: function (div) {
        var data = {};
        div.find("[DataField]").each(function (i) {
            data[$(this).attr("DataField")] = YXQCore.Form.GetControlData($(this));
        });
        return data;
    },
    SetDivFormData: function (div, jsonData) {
        div.find("[DataField]").each(function (i) {
            var dataField = $(this).attr("DataField");
            if (dataField != undefined) {
                YXQCore.Form.SetControlData($(this), jsonData[dataField]);
            }
        });
    },
     ClearDivData: function (div, jsonData) {
        div.find("[DataField]").each(function (i) {
            var dataField = $(this).attr("DataField");
            if (dataField != undefined) {
                YXQCore.Form.SetControlData($(this), "");
            }
        });
    },
    SetControlData: function (control, data) {
        switch ($(control).attr("tagName")) {
            case "INPUT":
                {
                    data += $(control).val(data);
                }
                break;
            case "SPAN":
                {
                    data += $(control).text(data);
                }
                break;
            default:
                data += $(control).val(data);
                break;
        }

    },
    GetControlData: function (control) {
        var data = "";
        switch ($(control).attr("tagName")) {
            case "TEXTAREA":
            case "INPUT":
                {
                    if ($(control).attr("type") == "checkbox") {
                        data += $(control).attr("checked");
                    }
                    else
                        data += $(control).val();

                }
                break;
            case "SPAN":
                {
                    data += $(control).text();
                }
                break;
            case "SELECT":
                {
                    data += $(control).val();
                }
                break; default:
                data += $(control).val();
                break;
        }
        return YXQCore.Form.ConvertToJson(data);
    },
    ConvertToJson: function (s) {
        s = s.replace(/(\\|\"|\')/g, "\\$1")
                    .replace(/\n|\r|\t/g,
                    function () {
                        var a = arguments[0];
                        return (a == '\n') ? '\\n' :
                           (a == '\r') ? '\\r' :
                           (a == '\t') ? '\\t' : ""
                    });
        return s;
    },
    ConverArryToString: function (s) {
        var newstring = '';
        if (s.length > 0) {
            for (var i = 0; i < s.length; i++) {
                newstring = newstring + s[i] + ",";
            }
        } else {
            newstring = s;
        }
        return newstring;
    },
   
    GetHfControlValue: function (o) {
        return $("#" + o).val();
    },
    SetHfControlValue: function (o, rowid) {
        $("#" + o).val(rowid);
    },
    QueryNormalForm: function (code, func) {

    }

}; 

  
Date.prototype.format = function(formatter) {
    if (!formatter || formatter == "") {
        formatter = "yyyy-MM-dd";
    }
    var year = this.getYear().toString();
    var month = (this.getMonth() + 1).toString();
    var day = this.getDate().toString();
    var yearMarker = formatter.replace(/[^y|Y]/g, '');
    if (yearMarker.length == 2) {
        year = year.substring(2, 4);
    }
    var monthMarker = formatter.replace(/[^m|M]/g, '');
    if (monthMarker.length > 1) {
        if (month.length == 1) {
            month = "0" + month;
        }
    }
    var dayMarker = formatter.replace(/[^d]/g, '');
    if (dayMarker.length > 1) {
        if (day.length == 1) {
            day = "0" + day;
        }
    }
    return formatter.replace(yearMarker, year).replace(monthMarker, month).replace(dayMarker, day);
}
Date.parseString = function(dateString, formatter) {
    var today = new Date();
    if (!dateString || dateString == "") {
        return today;
    }
    if (!formatter || formatter == "") {
        formatter = "yyyy-MM-dd";
    }
    var yearMarker = formatter.replace(/[^y|Y]/g, '');
    var monthMarker = formatter.replace(/[^m|M]/g, '');
    var dayMarker = formatter.replace(/[^d]/g, '');
    var yearPosition = formatter.indexOf(yearMarker);
    var yearLength = yearMarker.length;
    var year = dateString.substring(yearPosition, yearPosition + yearLength) * 1;
    if (yearLength == 2) {
        if (year < 50) {
            year += 2000;
        }
        else {
            year += 1900;
        }
    }
    var monthPosition = formatter.indexOf(monthMarker);
    var month = dateString.substring(monthPosition, monthPosition + monthMarker.length) * 1 - 1;
    var dayPosition = formatter.indexOf(dayMarker);
    var day = dateString.substring(dayPosition, dayPosition + dayMarker.length) * 1;
    return new Date(year, month, day);
}



 

YXQCore.Dialog = {
    DefaultDialog: null,
    DefaultOptions: {
        autoOpen: false,
        modal: true,
        bgiframe: true,
        height: 400,
        width: 750,
        title: "",
        close: function(event, ui) {
            //YXQCore.Dialog.DefaultDialog.empty();
        }
    },
    InitDialog: function() {

        $("#dialogSystem").dialog(YXQCore.Dialog.DefaultOptions);
        YXQCore.Dialog.DefaultDialog = $("#dialogSystem");
    },
    OpenDialog: function(options) {
        if (options != undefined) {
            $.each(YXQCore.Dialog.DefaultOptions, function(key, value) {
                if (options[key] == null)
                    options[key] = value;
            });
            YXQCore.Dialog.DefaultDialog.dialog("option", options);
        }
        YXQCore.Dialog.DefaultDialog.dialog("open");
    },
    CloseDialog: function() {
        YXQCore.Dialog.DefaultDialog.dialog("close");
    }




};


//时间格式化
Date.prototype.format = function (format) {
    /*
    * eg:format="yyyy-MM-dd hh:mm:ss";
    */
    if (!format) {
        format = "yyyy-MM-dd";
    }

    var o = {
        "M+": this.getMonth() + 1, // month
        "d+": this.getDate(), // day
        "h+": this.getHours(), // hour
        "m+": this.getMinutes(), // minute
        "s+": this.getSeconds(), // second
        "q+": Math.floor((this.getMonth() + 3) / 3), // quarter
        "S": this.getMilliseconds()
        // millisecond
    };

    if (/(y+)/.test(format)) {
        format = format.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
    }

    for (var k in o) {
        if (new RegExp("(" + k + ")").test(format)) {
            format = format.replace(RegExp.$1, RegExp.$1.length == 1 ? o[k] : ("00" + o[k]).substr(("" + o[k]).length));
        }
    }
    return format;
};



/*!
 
 * -------------------------------------------------------------------
 * $('#star').raty();
 *
 * <div id="star"></div>
 *
 */

; (function ($) {

    var methods = {
        init: function (settings) {
            return this.each(function () {
                var self = this,
					$this = $(self).empty();

                self.opt = $.extend(true, {}, $.fn.raty.defaults, settings);

                $this.data('settings', self.opt);

                self.opt.number = methods.between(self.opt.number, 0, 20);

                if (self.opt.path.substring(self.opt.path.length - 1, self.opt.path.length) != '/') {
                    self.opt.path += '/';
                }

                if (typeof self.opt.score == 'function') {
                    self.opt.score = self.opt.score.call(self);
                }

                if (self.opt.score) {
                    self.opt.score = methods.between(self.opt.score, 0, self.opt.number);
                }

                for (var i = 1; i <= self.opt.number; i++) {
                    $('<img />', {
                        src: self.opt.path + ((!self.opt.score || self.opt.score < i) ? self.opt.starOff : self.opt.starOn),
                        alt: i,
                        title: (i <= self.opt.hints.length && self.opt.hints[i - 1] !== null) ? self.opt.hints[i - 1] : i
                    }).appendTo(self);

                    if (self.opt.space) {
                        $this.append((i < self.opt.number) ? '&#160;' : '');
                    }
                }

                self.stars = $this.children('img:not(".raty-cancel")');
                self.score = $('<input />', { type: 'hidden', name: self.opt.scoreName }).appendTo(self);

                if (self.opt.score && self.opt.score > 0) {
                    self.score.val(self.opt.score);
                    methods.roundStar.call(self, self.opt.score);
                }

                if (self.opt.iconRange) {
                    methods.fill.call(self, self.opt.score);
                }

                methods.setTarget.call(self, self.opt.score, self.opt.targetKeep);

                var space = self.opt.space ? 4 : 0,
					width = self.opt.width || (self.opt.number * self.opt.size + self.opt.number * space);

                if (self.opt.cancel) {
                    self.cancel = $('<img />', { src: self.opt.path + self.opt.cancelOff, alt: 'x', title: self.opt.cancelHint, 'class': 'raty-cancel' });

                    if (self.opt.cancelPlace == 'left') {
                        $this.prepend('&#160;').prepend(self.cancel);
                    } else {
                        $this.append('&#160;').append(self.cancel);
                    }

                    width += (self.opt.size + space);
                }

                if (self.opt.readOnly) {
                    methods.fixHint.call(self);

                    if (self.cancel) {
                        self.cancel.hide();
                    }
                } else {
                    $this.css('cursor', 'pointer');

                    methods.bindAction.call(self);
                }

                $this.css('width', width);
            });
        }, between: function (value, min, max) {
            return Math.min(Math.max(parseFloat(value), min), max);
        }, bindAction: function () {
            var self = this,
				$this = $(self);

            $this.mouseleave(function () {
                var score = self.score.val() || undefined;

                methods.initialize.call(self, score);
                methods.setTarget.call(self, score, self.opt.targetKeep);

                if (self.opt.mouseover) {
                    self.opt.mouseover.call(self, score);
                }
            });

            var action = self.opt.half ? 'mousemove' : 'mouseover';

            if (self.opt.cancel) {
                self.cancel.mouseenter(function () {
                    $(this).attr('src', self.opt.path + self.opt.cancelOn);

                    self.stars.attr('src', self.opt.path + self.opt.starOff);

                    methods.setTarget.call(self, null, true);

                    if (self.opt.mouseover) {
                        self.opt.mouseover.call(self, null);
                    }
                }).mouseleave(function () {
                    $(this).attr('src', self.opt.path + self.opt.cancelOff);

                    if (self.opt.mouseover) {
                        self.opt.mouseover.call(self, self.score.val() || null);
                    }
                }).click(function (evt) {
                    self.score.removeAttr('value');

                    if (self.opt.click) {
                        self.opt.click.call(self, null, evt);
                    }
                });
            }

            self.stars.bind(action, function (evt) {
                var value = parseInt(this.alt, 10);

                if (self.opt.half) {
                    var position = parseFloat((evt.pageX - $(this).offset().left) / self.opt.size),
						diff = (position > .5) ? 1 : .5;

                    value = parseFloat(this.alt) - 1 + diff;

                    methods.fill.call(self, value);

                    if (self.opt.precision) {
                        value = value - diff + position;
                    }

                    methods.showHalf.call(self, value);
                } else {
                    methods.fill.call(self, value);
                }

                $this.data('score', value);

                methods.setTarget.call(self, value, true);

                if (self.opt.mouseover) {
                    self.opt.mouseover.call(self, value, evt);
                }
            }).click(function (evt) {
                self.score.val((self.opt.half || self.opt.precision) ? $this.data('score') : this.alt);

                if (self.opt.click) {
                    self.opt.click.call(self, self.score.val(), evt);
                }
            });
        }, cancel: function (isClick) {
            return $(this).each(function () {
                var self = this,
					$this = $(self);

                if ($this.data('readonly') === true) {
                    return this;
                }

                if (isClick) {
                    methods.click.call(self, null);
                } else {
                    methods.score.call(self, null);
                }

                self.score.removeAttr('value');
            });
        }, click: function (score) {
            return $(this).each(function () {
                if ($(this).data('readonly') === true) {
                    return this;
                }

                methods.initialize.call(this, score);

                if (this.opt.click) {
                    this.opt.click.call(this, score);
                } else {
                    methods.error.call(this, 'you must add the "click: function(score, evt) { }" callback.');
                }

                methods.setTarget.call(this, score, true);
            });
        }, error: function (message) {
            $(this).html(message);

            $.error(message);
        }, fill: function (score) {
            var self = this,
				number = self.stars.length,
				count = 0,
				$star,
				star,
				icon;

            for (var i = 1; i <= number; i++) {
                $star = self.stars.eq(i - 1);

                if (self.opt.iconRange && self.opt.iconRange.length > count) {
                    star = self.opt.iconRange[count];

                    if (self.opt.single) {
                        icon = (i == score) ? (star.on || self.opt.starOn) : (star.off || self.opt.starOff);
                    } else {
                        icon = (i <= score) ? (star.on || self.opt.starOn) : (star.off || self.opt.starOff);
                    }

                    if (i <= star.range) {
                        $star.attr('src', self.opt.path + icon);
                    }

                    if (i == star.range) {
                        count++;
                    }
                } else {
                    if (self.opt.single) {
                        icon = (i == score) ? self.opt.starOn : self.opt.starOff;
                    } else {
                        icon = (i <= score) ? self.opt.starOn : self.opt.starOff;
                    }

                    $star.attr('src', self.opt.path + icon);
                }
            }
        }, fixHint: function () {
            var $this = $(this),
				score = parseInt(this.score.val(), 10),
				hint = this.opt.noRatedMsg;

            if (!isNaN(score) && score > 0) {
                hint = (score <= this.opt.hints.length && this.opt.hints[score - 1] !== null) ? this.opt.hints[score - 1] : score;
            }

            $this.data('readonly', true).css('cursor', 'default').attr('title', hint);

            this.score.attr('readonly', 'readonly');
            this.stars.attr('title', hint);
        }, getScore: function () {
            var score = [],
				value;

            $(this).each(function () {
                value = this.score.val();

                score.push(value ? parseFloat(value) : undefined);
            });

            return (score.length > 1) ? score : score[0];
        }, readOnly: function (isReadOnly) {
            return this.each(function () {
                var $this = $(this);

                if ($this.data('readonly') === isReadOnly) {
                    return this;
                }

                if (this.cancel) {
                    if (isReadOnly) {
                        this.cancel.hide();
                    } else {
                        this.cancel.show();
                    }
                }

                if (isReadOnly) {
                    $this.unbind();

                    $this.children('img').unbind();

                    methods.fixHint.call(this);
                } else {
                    methods.bindAction.call(this);
                    methods.unfixHint.call(this);
                }

                $this.data('readonly', isReadOnly);
            });
        }, reload: function () {
            return methods.set.call(this, {});
        }, roundStar: function (score) {
            var diff = (score - Math.floor(score)).toFixed(2);

            if (diff > this.opt.round.down) {
                var icon = this.opt.starOn;								// Full up: [x.76 .. x.99]

                if (diff < this.opt.round.up && this.opt.halfShow) {	// Half: [x.26 .. x.75]
                    icon = this.opt.starHalf;
                } else if (diff < this.opt.round.full) {				// Full down: [x.00 .. x.5]
                    icon = this.opt.starOff;
                }

                this.stars.eq(Math.ceil(score) - 1).attr('src', this.opt.path + icon);
            }															// Full down: [x.00 .. x.25]
        }, score: function () {
            return arguments.length ? methods.setScore.apply(this, arguments) : methods.getScore.call(this);
        }, set: function (settings) {
            this.each(function () {
                var $this = $(this),
					actual = $this.data('settings'),
					clone = $this.clone().removeAttr('style').insertBefore($this);

                $this.remove();

                clone.raty($.extend(actual, settings));
            });

            return $(this.selector);
        }, setScore: function (score) {
            return $(this).each(function () {
                if ($(this).data('readonly') === true) {
                    return this;
                }

                methods.initialize.call(this, score);
                methods.setTarget.call(this, score, true);
            });
        }, setTarget: function (value, isKeep) {
            if (this.opt.target) {
                var $target = $(this.opt.target);
                
                if ($target.length == 0) {
                    methods.error.call(this, 'target selector invalid or missing!');
                }

                var score = value;

                if (!isKeep || score === undefined) {
                    score = this.opt.targetText;
                } else {
                    if (this.opt.targetType == 'hint') {
                        score = (score === null && this.opt.cancel)
								? this.opt.cancelHint
								: this.opt.hints[Math.ceil(score - 1)];
                    } else {
                        score = this.opt.precision
								? parseFloat(score).toFixed(1)
								: parseInt(score, 10);
                    }
                }

                if (this.opt.targetFormat.indexOf('{score}') < 0) {
                    methods.error.call(this, 'template "{score}" missing!');
                }

                if (value !== null) {
                    score = this.opt.targetFormat.toString().replace('{score}', score);
                }

                if ($target.is(':input')) {
                    $target.val(score);
                } else {
                    $target.html(score);
                }
            }
        }, showHalf: function (score) {
            var diff = (score - Math.floor(score)).toFixed(1);

            if (diff > 0 && diff < .6) {
                this.stars.eq(Math.ceil(score) - 1).attr('src', this.opt.path + this.opt.starHalf);
            }
        }, initialize: function (score) {
            score = !score ? 0 : methods.between(score, 0, this.opt.number);

            methods.fill.call(this, score);

            if (score > 0) {
                if (this.opt.halfShow) {
                    methods.roundStar.call(this, score);
                }

                this.score.val(score);
            }
        }, unfixHint: function () {
            for (var i = 0; i < this.opt.number; i++) {
                this.stars.eq(i).attr('title', (i < this.opt.hints.length && this.opt.hints[i] !== null) ? this.opt.hints[i] : i);
            }

            $(this).data('readonly', false).css('cursor', 'pointer').removeAttr('title');

            this.score.attr('readonly', 'readonly');
        }
    };

    $.fn.raty = function (method) {
        if (methods[method]) {
            return methods[method].apply(this, Array.prototype.slice.call(arguments, 1));
        } else if (typeof method === 'object' || !method) {
            return methods.init.apply(this, arguments);
        } else {
            $.error('Method ' + method + ' does not exist!');
        }
    };

    $.fn.raty.defaults = {
        cancel: false,
        cancelHint: 'cancel this rating!',
        cancelOff: 'cancel-off.png',
        cancelOn: 'cancel-on.png',
        cancelPlace: 'left',
        click: undefined,
        half: false,
        halfShow: true,
        hints: ['bad', 'poor', 'regular', 'good', 'gorgeous'],
        iconRange: undefined,
        mouseover: undefined,
        noRatedMsg: 'not rated yet',
        number: 5,
        path: 'img/',
        precision: false,
        round: { down: .25, full: .6, up: .76 },
        readOnly: false,
        score: undefined,
        scoreName: 'score',
        single: false,
        size: 16,
        space: true,
        starHalf: 'star-half.png',
        starOff: 'star-off.png',
        starOn: 'star-on.png',
        target: undefined,
        targetFormat: '{score}',
        targetKeep: false,
        targetText: '',
        targetType: 'hint',
        width: undefined
    };

})(jQuery);


function Drag() {
    //初始化
    this.initialize.apply(this, arguments)
}
Drag.prototype = {
    //初始化
    initialize: function (drag, options) {
        this.drag = this.$(drag);
        this._x = this._y = 0;
        this._moveDrag = this.bind(this, this.moveDrag);
        this._stopDrag = this.bind(this, this.stopDrag);

        this.setOptions(options);

        this.handle = this.$(this.options.handle);
        this.maxContainer = this.$(this.options.maxContainer);

        this.maxTop = Math.max(this.maxContainer.clientHeight, this.maxContainer.scrollHeight) - this.drag.offsetHeight;
        this.maxLeft = Math.max(this.maxContainer.clientWidth, this.maxContainer.scrollWidth) - this.drag.offsetWidth;

        this.limit = this.options.limit;
        this.lockX = this.options.lockX;
        this.lockY = this.options.lockY;
        this.lock = this.options.lock;

        this.onStart = this.options.onStart;
        this.onMove = this.options.onMove;
        this.onStop = this.options.onStop;

        this.handle.style.cursor = "move";

        this.changeLayout();

        this.addHandler(this.handle, "mousedown", this.bind(this, this.startDrag))
    },
    changeLayout: function () {
        this.drag.style.top = this.drag.offsetTop + "px";
        this.drag.style.left = this.drag.offsetLeft + "px";
        this.drag.style.position = "absolute";
        this.drag.style.margin = "0"
    },
    startDrag: function (event) {
        var event = event || window.event;

        this._x = event.clientX - this.drag.offsetLeft;
        this._y = event.clientY - this.drag.offsetTop;

        this.addHandler(document, "mousemove", this._moveDrag);
        this.addHandler(document, "mouseup", this._stopDrag);

        event.preventDefault && event.preventDefault();
        this.handle.setCapture && this.handle.setCapture();

        this.onStart()
    },
    moveDrag: function (event) {
        var event = event || window.event;

        var iTop = event.clientY - this._y;
        var iLeft = event.clientX - this._x;

        if (this.lock) return;

        this.limit && (iTop < 0 && (iTop = 0), iLeft < 0 && (iLeft = 0), iTop > this.maxTop && (iTop = this.maxTop), iLeft > this.maxLeft && (iLeft = this.maxLeft));

        this.lockY || (this.drag.style.top = iTop + "px");
        this.lockX || (this.drag.style.left = iLeft + "px");

        event.preventDefault && event.preventDefault();

        this.onMove()
    },
    stopDrag: function () {
        this.removeHandler(document, "mousemove", this._moveDrag);
        this.removeHandler(document, "mouseup", this._stopDrag);

        this.handle.releaseCapture && this.handle.releaseCapture();

        this.onStop()
    },
    //参数设置
    setOptions: function (options) {
        this.options =
        {
            handle: this.drag, //事件对象
            limit: true, //锁定范围
            lock: false, //锁定位置
            lockX: false, //锁定水平位置
            lockY: false, //锁定垂直位置
            maxContainer: document.documentElement || document.body, //指定限制容器
            onStart: function () { }, //开始时回调函数
            onMove: function () { }, //拖拽时回调函数
            onStop: function () { }  //停止时回调函数
        };
        for (var p in options) this.options[p] = options[p]
    },
    //获取id
    $: function (id) {
        return typeof id === "string" ? document.getElementById(id) : id
    },
    //添加绑定事件
    addHandler: function (oElement, sEventType, fnHandler) {
        return oElement.addEventListener ? oElement.addEventListener(sEventType, fnHandler, false) : oElement.attachEvent("on" + sEventType, fnHandler)
    },
    //删除绑定事件
    removeHandler: function (oElement, sEventType, fnHandler) {
        return oElement.removeEventListener ? oElement.removeEventListener(sEventType, fnHandler, false) : oElement.detachEvent("on" + sEventType, fnHandler)
    },
    //绑定事件到对象
    bind: function (object, fnHandler) {
        return function () {
            return fnHandler.apply(object, arguments)
        }
    }
};
