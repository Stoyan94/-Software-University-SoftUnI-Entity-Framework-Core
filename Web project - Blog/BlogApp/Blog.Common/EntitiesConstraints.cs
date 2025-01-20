namespace Blog.Common
{
    public static class EntitiesConstraints
    {
        public const byte ArticleTitleMaxLength = 50;
        public const byte ArticleTitleMinLength = 10;
        public const short ArticleContentMaxLength = 1500;
        public const byte ArticleContentMinLength = 50;
        public const byte ArticleAuthorMaxLength = 15;

        public const byte ApplicationUserNameMaxLength = 20;
        public const byte ApplicationUserNameMinLength = 5;
        public const byte ApplicationUserEmailMaxLength = 50;
        public const byte ApplicationUserEmailMinLength = 10;

    }
}
