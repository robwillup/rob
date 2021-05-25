using System;
using Microsoft.AspNetCore.Components;

namespace Rob.Pages 
{
    public partial class About : ComponentBase
    {
        public string TabID { get; set; }

        protected override void OnInitialized()
        {
            TabID = "summary";
        }

        private void SetTabID(string id)
        {            
            TabID = id;
        }
    }
}