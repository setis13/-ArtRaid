namespace ArtRaid.Dtos {
    public class WebArtDto {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ArtId { get; set; }
        public short? HeroId { get; set; }
        public byte? Level { get; set; }
        public short Order { get; set; }
        public string Comment { get; set; }
    }
}
