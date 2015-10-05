
function addEventSimple(obj,evt,fn){
	if(obj.addEventListener){
		obj.addEventListener(evt,fn,false);
	}else if(obj.attachEvent){
		obj.attachEvent('on'+evt,fn);
	}
}

addEventSimple(window,'load',initScrolling);

var scrollingBox;
var scrollingInterval;
var reachedBottom=false;
var bottom;

function initScrolling(){
	scrollingBox = document.getElementById('scroll');
	scrollingBox.style.overflow = "hidden";
	scrollingInterval = setInterval("scrolling()",50);
	scrollingBox.onmouseover = over;
	scrollingBox.onmouseout = out; 
}

function scrolling(){
	var origin = scrollingBox.scrollTop++;
	if(origin == scrollingBox.scrollTop){
		if(!reachedBottom){
			scrollingBox.innerHTML+=scrollingBox.innerHTML;
			reachedBottom=true;
			bottom=origin;
		}else{
			scrollingBox.scrollTop=bottom;
		}
	}
}

function over(){
	clearInterval(scrollingInterval);
}
function out(){
	scrollingInterval = setInterval("scrolling()",50);
}

