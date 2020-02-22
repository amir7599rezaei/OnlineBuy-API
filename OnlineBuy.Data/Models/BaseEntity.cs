using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OnlineBuy.Data.Models
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T">For type of id</typeparam>
    public class BaseEntity<T>
    {        
        [Key]
        public T Id { get; set; }        
        public DateTime DateCreated { get; set; }        
        public DateTime DateModified { get; set; }        
        public DateTime DateDeleted { get; set; }
    }
}
