using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LLM_Interaction.Models
{
    public class Provider
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ApiUrl { get; set; }
        public string ApiKey { get; set; }

        public virtual ICollection<Model> Models { get; set; }
    }


}