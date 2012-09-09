using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Lab2.Models.Entities.Abstract;
using System.ComponentModel;

namespace Lab2.Models.Entities
{
    public class ForumThread : IEntity
    {
        public Guid ID { get; set; }

        [DisplayName("Thread title")]
        public string Title { get; set; }
        
        [DisplayName("Created")]
        public DateTime CreateDate { get; set; }
    }
}