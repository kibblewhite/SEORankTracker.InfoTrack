namespace SEORankTracker.Database.Models;

public class SearchEntry : BaseEntity
{
    public required string Url { get; set; }
    public required string SearchEngine { get; set; }
    public required string SearchTerm { get; set; }
    public required string MatchTerm { get; set; }
    public required string RankListCsv { get; set; }
    public required int Occurrences { get; set; }
    public required bool Success { get; set; }
    public required string Error { get; set; }
    public required DateTimeOffset UtcDateTime { get; set; }
}

public class SearchEntryConfiguration() : BaseEntityConfiguration<SearchEntry>
{
    public override void Configure(EntityTypeBuilder<SearchEntry> builder)
    {
        builder.Property(x => x.Url).IsRequired(true);
        builder.Property(x => x.SearchEngine).IsRequired(true);
        builder.Property(x => x.SearchTerm).IsRequired(true);
        builder.Property(x => x.MatchTerm).IsRequired(true);
        builder.Property(x => x.RankListCsv).IsRequired(true);
        builder.Property(x => x.Occurrences).IsRequired(true);
        builder.Property(x => x.Success).IsRequired(true);
        builder.Property(x => x.Error).IsRequired(true);
        builder.Property(x => x.UtcDateTime).IsRequired(true);
    }
}
