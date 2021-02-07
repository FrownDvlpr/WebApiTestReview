using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.services.extension;

namespace WebApplication1.Models
{
    public class Tasks
    {
        public Guid ID { get; set; }
        public DateTime LastTime { get; set; }
        public TaskStatus Status { get; set; }
    }

    public enum TaskStatus
    {
        [EnumCustomAttributesDescription(description: "создана")]
        Created = 1,
        [EnumCustomAttributesDescription(description: "исполнение")]
        Running = 2,
        [EnumCustomAttributesDescription(description: "завершена")]
        Finished = 3,
    }
}