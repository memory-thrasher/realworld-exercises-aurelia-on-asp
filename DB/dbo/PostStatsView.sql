CREATE VIEW [dbo].[poststatsview] as
with cte_Users as (select Username from Users)
,cte_Email as (select username, Email from users)
,cte_First_post as (select authorusername, min(CreatedAt) First_Post from articles group by AuthorUsername)
,cte_Last_Post as (select authorusername, max(CreatedAt) Last_Post from articles group by authorusername)
,cte_Article_Count as (select authorusername, count(id) Article_Count from Articles group by authorusername)
,cte_Distinct_Articles as (select authorusername, count(body)-count(distinct body) Distinct_Articles from Articles group by authorusername)
,cte_First_Comment as (select username, min(createdat) First_Comment from comments group by username)
,cte_Last_Comment as (select username, max(createdat) Last_Comment from Comments group by username)
,cte_Comment_Count as (select username, count(body) Comment_Count from Comments group by username)
,cte_Deleted_Comments as (select username, count(DeletedAt) Deleted_Comments from comments group by username)
,cte_Comments_Own_Post as (select comments.username, count(comments.Username) Comments_Own_Post from Comments 
join articles on comments.ArticleId=articles.id where comments.username=articles.authorusername group by username)
select users.username, cte_email.Email, cte_first_post.First_Post, cte_Last_Post.Last_Post, cte_Article_Count.Article_Count, cte_Distinct_Articles.Distinct_Articles, cte_First_Comment.First_Comment, cte_Last_Comment.Last_Comment, cte_Comment_Count.Comment_Count, cte_Deleted_Comments.Deleted_Comments, cte_Comments_Own_Post.Comments_Own_Post from Users
left join cte_Email on cte_email.username=users.username
left join cte_First_Post on  cte_first_post.authorusername=users.username
left join cte_Last_Post on cte_Last_Post.authorusername=users.username
left join cte_Article_Count on cte_Article_Count.AuthorUsername=users.Username 
left join cte_Distinct_Articles on cte_Distinct_Articles.AuthorUsername=Users.username
left join cte_First_Comment on cte_First_Comment.Username=users.username
left join cte_Last_Comment on cte_Last_comment.username=users.username
left join cte_Comment_Count on cte_Comment_Count.username=users.username
left join cte_Deleted_Comments on cte_Deleted_Comments.username=users.username
left join cte_Comments_Own_Post on cte_Comments_Own_Post.username=users.Username

