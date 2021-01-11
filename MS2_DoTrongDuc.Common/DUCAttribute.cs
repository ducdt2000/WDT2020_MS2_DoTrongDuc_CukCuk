using System;
using System.Collections.Generic;
using System.Text;

namespace MS2_DoTrongDuc.Common
{
    [AttributeUsage(AttributeTargets.Property)]
    public class DUCAttribute : Attribute
    {
        /// <summary>
        /// Tên thuộc tính
        /// </summary>
        public string PropertyName;

        /// <summary>
        /// Thông báo
        /// </summary>
        public string Messenger;
        protected DUCAttribute(string propertyName, string messenger = "")
        {
            this.PropertyName = propertyName;
            this.Messenger = messenger;
        }
    }

    /// <summary>
    /// Attribute để xác định việc bắt buộc nhập
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class Required : DUCAttribute
    {
        public Required(string propertyName) : base(propertyName, $"{propertyName} không được phép để trống. ")
        {
        }
    }

    /// <summary>
    /// Attribute xác định việc trùng lặp
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class Duplicate : DUCAttribute
    {
        public Duplicate(string propertyName) : base(propertyName, $"Trùng {propertyName}. ")
        {
        }
    }

    /// <summary>
    /// Attribute xác định việc giới hạn lớn nhất độ dài của thuộc tính
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class MaxLength: DUCAttribute
    {
        /// <summary>
        /// Độ dài lớn nhất có thể có
        /// </summary>
        public int LengthMax;

        public MaxLength(string propertyName, int length = int.MaxValue):base(propertyName, $"Độ dài tối đa của {propertyName} là {length}. ")
        {
            this.LengthMax = length;
        }
    }

    /// <summary>
    /// Attribute xác định viện giới hạn nhỏ nhất độ dài của thuộc tính
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class MinLength: DUCAttribute
    {
        /// <summary>
        /// Độ dài nhỏ nhất có thể có
        /// </summary>
        public int LengthMin;
        public MinLength(string propertyName, int length = int.MinValue):base(propertyName, $"Độ dài tối thiểu của {propertyName} là {length}. ")
        {
            this.LengthMin = length;
        }
    }

    /// <summary>
    /// Attribute xác định việc chiều dài nằm trong 1 danh sách số nguyên dương
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class Length: DUCAttribute
    {
        /// <summary>
        /// Danh sách các chiều dài cho phép 
        /// </summary>
        public int[] LengthList;
        public Length(string propertyName, int[] lengthList = null):base(propertyName, $"{propertyName} phải có chiều dài là {String.Join(",", Array.ConvertAll(lengthList, el => el.ToString()))}. ")
        {
            this.LengthList = lengthList;
        }
    }

    /// <summary>
    /// Check đúng kiểu dữ liệu theo Regex
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class TypeOf : DUCAttribute
    {
        public string strRegex;
        public TypeOf(string propertyName) : base(propertyName, $"Không đúng kiểu {propertyName}. ")
        {
            if(propertyName == "Email")
            {
                this.strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                  @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                  @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            }
        }
    }
   
}
