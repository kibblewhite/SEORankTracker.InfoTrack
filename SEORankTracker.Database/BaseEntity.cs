namespace SEORankTracker.Database;

public abstract class BaseEntity
{
    public required int Id { get; init; }

    protected BaseEntity() { }

    protected BaseEntity(int id) : this() => Id = id;
}

public abstract class BaseEntityConfiguration<TEntityType> : IEntityTypeConfiguration<TEntityType> where TEntityType : BaseEntity
{
    public virtual void Configure(EntityTypeBuilder<TEntityType> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasIndex(x => x.Id).IsUnique();
        builder.Property(x => x.Id).IsRequired();
        builder.Property(x => x.Id).HasConversion<int>();
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
    }

    void IEntityTypeConfiguration<TEntityType>.Configure(EntityTypeBuilder<TEntityType> builder) => Configure(builder);
}
