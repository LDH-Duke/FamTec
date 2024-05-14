namespace FamTec.Shared.DTO
{
    public class BuildingsDTO
    {
        public bool IndexChk { get; set; } = false;

        public string BuildingCd { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public int? FloorNum { get; set; }

        public DateTime? CompletionDate { get; set; }
             
        public DateTime? CreateDt { get; set; }

    }
}
