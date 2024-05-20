using System.ComponentModel.DataAnnotations;

namespace FamTec.Shared.DTO
{
    /// <summary>
    /// 사업장 DTO
    /// </summary>
    public class PlacesDTO
    {
        public bool IsSelect { get; set; } = false;

        /// <summary>
        /// 사업장 코드
        /// </summary>
        [Display(Name = "사업장코드")]
        public string? PlaceCd { get; set; }

        /// <summary>
        /// 사업장 명
        /// </summary>
        [Display(Name = "사업장명")]
        public string? Name { get; set; }

        /// <summary>
        /// 계약번호
        /// </summary>
        [Display(Name = "계약번호")]
        public string? CONTRACT_NUM { get; set; }

        /// <summary>
        /// 비고
        /// </summary>
        [Display(Name = "비고")]
        public string? NOTE { get; set; }

        /// <summary>
        /// 계약일자
        /// </summary>
        [Display(Name = "계약일자")]
        public DateTime CONTRACT_DT { get; set; }

        /// <summary>
        /// 계약상태
        /// </summary>
        [Display(Name = "계약상태")]
        public int? STATUS { get; set; }

    }

}
