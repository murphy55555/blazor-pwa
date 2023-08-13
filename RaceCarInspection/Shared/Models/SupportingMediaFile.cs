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
        public MediaFileType MediaFileType { get; set; }
        public IBrowserFile IBrowserFile { get; set; }
        public string ThumbnailBase64 { get; set; }
        public string CapturedBase64Data { get; set; }
        public DateTime CaptureStartTime { get; set; }
        public int LengthInSeconds { get; set; }
    }
}
