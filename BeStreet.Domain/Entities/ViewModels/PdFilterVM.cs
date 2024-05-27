namespace BeStreet.Domain.Entities.ViewModels
{
    public class PdFilterVM
    {
        public int PdId { get; set; }

        public int ColorId { get; set; }
        public string ColorName { get; set; }

        public int SizeId { get; set; }
        public string SizeName { get; set; }

        public string PdName { get; set; }
        public string PdDtls { get; set; }

        public double PdPrice { get; set; }

        public int PdStk { get; set; }

        public int PdtId { get; set; }
        public string PdtName { get; set; }

        public int StatusId { get; set; }

        public string StatusName { get; set; }



        public int TargetId { get; set; }

        public string TargetName { get; set; }


        public int SupId { get; set; }

    }
}
