namespace BackForLab_3.Models.Dto
{
    public class TreeDto
    {
        public string Label { get; set; } = null!;
        public string Value { get; set; } = null!;
        public List<TreeDto>? Children { get; set; } = null!;
    }
}