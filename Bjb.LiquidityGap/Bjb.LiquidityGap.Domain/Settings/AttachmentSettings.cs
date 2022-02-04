using System;
using System.Collections.Generic;
using System.Text;

namespace Bjb.LiquidityGap.Domain.Settings
{
    public class AttachmentSettings
    {
        public string UploadPath { get; set; }
        public string BaseUrl { get; set; }
        public string ApiPublicUrl { get; set; }
        public bool UseFileService { get; set; }
    }
}
