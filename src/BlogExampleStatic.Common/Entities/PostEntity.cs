using System;

namespace BlogExampleStatic.Common.Entities
{
    public class PostEntity
    {
        public long Id { get; set; }
        public DateTime CreateDate { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }

        public ApplicationUserEntity Author { get; set; }
    }
}