namespace BeStreet.Domain.Entities.ViewModels
{
    public class CtdVM
    {
        public int CartId { get; set; }

        public int PdId { get; set; }

        public string PdName { get; set; }
        public string ColorName { get; set; }
        public string SizeName { get; set; }

        public int? CdtlQty { get; set; }

        public double? CdtlPrice { get; set; }

        public double? CdtlMoney { get; set; }
    }
}
