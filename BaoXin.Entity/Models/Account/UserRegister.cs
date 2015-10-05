using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace BaoXin.Entity.Models.Account
{
    public class UserRegister
    {
        /// <summary>
        /// 密码
        /// </summary>
        [Required(ErrorMessage = "必填项")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "长度必须为:6-20")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "密码和确认密码不匹配。")]
        public string ConfirmPassword { get; set; }
        ///// <summary>
        ///// 验证码
        ///// </summary>
        //[Required(ErrorMessage = "×")]
        //[StringLength(6, MinimumLength = 6, ErrorMessage = "×")]
        //public string VerificationCode { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        [Required(ErrorMessage = "必填项")]
        [StringLength(6, MinimumLength = 2, ErrorMessage = "长度必须为:2-6")]
        public string Nickname { get; set; }
        /// <summary>
        /// 邮箱地址
        /// </summary>
        [Required(ErrorMessage = "必填项")]
        [RegularExpression(@"(\w)+(\.\w+)*@(\w)+((\.\w+)+)", ErrorMessage = "{0}格式不正确")]
        public string Email { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        public string Mobile { get; set; }
        /// <summary>
        /// 联系qq
        /// </summary>
        public string Contactqq { get; set; }
        /// <summary>
        /// 联系地址
        /// </summary>
        public string Contactaddr { get; set; }

        /// <summary>
        /// 联系地址
        /// </summary>
        public string ImageUrl { get; set; }

    }
}
