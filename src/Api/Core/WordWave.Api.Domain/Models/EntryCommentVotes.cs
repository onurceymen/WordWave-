using WordWave.Api.Common.ViewModels;

namespace WordWave.Api.Domain.Models;


public class EntryCommentVote : BaseEntity
{
    public Guid EntryCommentId { get; set; }

    public VoteType VoteType { get; set; }

    public Guid CreatedById { get; set; }


    public virtual EntryComments EntryComment { get; set; }
}