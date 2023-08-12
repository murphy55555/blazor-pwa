using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaceCarInspection.Shared.Models
{
    public class SupportingMediaFile
    {
        public IBrowserFile IBrowserFile { get; set; }
        public string ThumbnailBase64 { get; set; }
    }
}
