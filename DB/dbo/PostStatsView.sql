CREATE VIEW [dbo].[poststatsview] as
with cte_Articles as (
	select AuthorUsername, 
		min(CreatedAt) First_Post, 
		max(CreatedAt) Last_Post, 
		count(id) Article_Count, 
		count(distinct body) Distinct_Articles 
	from articles 
	group by AuthorUsername)
,cte_Comments as (
	select Username, 
		min(createdat) First_Comment, 
		max(createdat) Last_Comment, 
		count(body) Comment_Count, 
		count(DeletedAt) Deleted_Comments, 
		count(comments.Username) Comments_Own_Post 
	from comments 
	group by username) 
,cte_Comments_Own as (
	select Username,
		count(Username) Comments_Own_Post 
	from comments
	join articles 
		on comments.ArticleId=articles.id 
	where comments.username=articles.authorusername 
	group by username)
select U.Username, 
	U.Email, 
	A.First_Post, 
	A.Last_Post, 
	A.Article_Count, 
	A.Distinct_Articles, 
	C.First_Comment, 
	C.Last_Comment, 
	C.Comment_Count, 
	C.Deleted_Comments, 
	CO.Comments_Own_Post
from Users as U
left join cte_Articles as A on A.AuthorUsername=U.username
left join cte_Comments as C on C.Username=U.username
left join cte_Comments_Own as CO on CO.Username=U.username
