using System;
using System.Collections.Generic;
using System.Text;

namespace BaoXin.Entity.Result
{
    public class TResult<T>
    {
        private bool _IsSuccess = false;
        public bool IsSuccess
        {
            get
            {
                return _IsSuccess;
            }
            set
            {
                _IsSuccess = value;
            }
        }

        private string _Message = "未知错误";
        public string Message
        {
            get
            {
                return _Message;
            }
            set
            {
                _Message = value;
            }
        }

        private T _TData;
        public T TData
        {
            get
            {
                return _TData;
            }
            set
            {
                _TData = value;
            }
        }

    }
}
