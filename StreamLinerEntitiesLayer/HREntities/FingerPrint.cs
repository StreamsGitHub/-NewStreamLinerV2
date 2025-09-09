using StreamLinerEntitiesLayer.Entities;

namespace StreamLinerEntitiesLayer.HREntities
{
    public class FingerPrint : MasterModel
    {
        public int FingerPrintId { get; set; }
        public int UploadFileId { get; set; }
        public string? ACNo { get; set; }
        public string? Name { get; set; }
        public string? Number { get; set; }
        public DateTime? DateAndTime { get; set; }
        public string? Status { get; set; }
        public string? NewState { get; set; }
        public string? Exception { get; set; }
        public string? Operation { get; set; }
        public string? IconUpload { get; set; }
    }
}