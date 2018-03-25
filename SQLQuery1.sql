select * from AspNetUsers
select * from AspNetRoles
select u.Email, r.Name from AspNetUserRoles ur
join AspNetUsers u on ur.UserId = u.Id
join AspNetRoles r on ur.RoleId = r.Id