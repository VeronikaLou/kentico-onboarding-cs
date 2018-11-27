Â
–C:\Users\VeronikaL\Desktop\kentico-onboarding-cs\KenticoOnboardingApplication\src\KenticoOnboardingApplication.Repositories\Properties\AssemblyInfo.cs
[ 
assembly 	
:	 

AssemblyTitle 
( 
$str D
)D E
]E F
[ 
assembly 	
:	 

AssemblyDescription 
( 
$str !
)! "
]" #
[		 
assembly		 	
:			 
!
AssemblyConfiguration		  
(		  !
$str		! #
)		# $
]		$ %
[

 
assembly

 	
:

	 

AssemblyCompany

 
(

 
$str

 
)

 
]

 
[ 
assembly 	
:	 

AssemblyProduct 
( 
$str F
)F G
]G H
[ 
assembly 	
:	 

AssemblyCopyright 
( 
$str 0
)0 1
]1 2
[ 
assembly 	
:	 

AssemblyTrademark 
( 
$str 
)  
]  !
[ 
assembly 	
:	 

AssemblyCulture 
( 
$str 
) 
] 
[ 
assembly 	
:	 


ComVisible 
( 
false 
) 
] 
[ 
assembly 	
:	 

Guid 
( 
$str 6
)6 7
]7 8
["" 
assembly"" 	
:""	 

AssemblyVersion"" 
("" 
$str"" $
)""$ %
]""% &
[## 
assembly## 	
:##	 

AssemblyFileVersion## 
(## 
$str## (
)##( )
]##) *®
—C:\Users\VeronikaL\Desktop\kentico-onboarding-cs\KenticoOnboardingApplication\src\KenticoOnboardingApplication.Repositories\RepositoriesBootstrapper.cs
	namespace 	(
KenticoOnboardingApplication
 &
.& '
Repositories' 3
{		 
public

 

class

 $
RepositoriesBootstrapper

 )
:

* +
IBootstrapper

, 9
{ 
public 
IUnityContainer 
Register '
(' (
IUnityContainer( 7
	container8 A
)A B
=>C E
	container 
. 
RegisterType "
<" #
IListRepository# 2
,2 3
Repositories4 @
.@ A
ListRepositoryA O
>O P
(P Q
newQ T'
HierarchicalLifetimeManagerU p
(p q
)q r
)r s
;s t
} 
} Ä 
šC:\Users\VeronikaL\Desktop\kentico-onboarding-cs\KenticoOnboardingApplication\src\KenticoOnboardingApplication.Repositories\Repositories\ListRepository.cs
	namespace 	(
KenticoOnboardingApplication
 &
.& '
Repositories' 3
.3 4
Repositories4 @
{		 
internal

 
class

 
ListRepository

 !
:

" #
IListRepository

$ 3
{ 
private 
static 
IMongoCollection '
<' (
Item( ,
>, -
_collection. 9
;9 :
public 
ListRepository 
( 
IConnectionString /
connectionString0 @
)@ A
{ 	
var 
urlConnectionString #
=$ %
MongoUrl& .
.. /
Create/ 5
(5 6
connectionString6 F
.F G
TodoListG O
)O P
;P Q
var 
client 
= 
new 
MongoClient (
(( )
urlConnectionString) <
)< =
;= >
var 
db 
= 
client 
. 
GetDatabase '
(' (
urlConnectionString( ;
.; <
DatabaseName< H
)H I
;I J
_collection 
= 
db 
. 
GetCollection *
<* +
Item+ /
>/ 0
(0 1
$str1 8
)8 9
;9 :
} 	
public 
async 
Task 
< 
IEnumerable %
<% &
Item& *
>* +
>+ ,
GetAllItemsAsync- =
(= >
)> ?
=>@ B
await 
_collection 
. 
Find "
(" #
FilterDefinition# 3
<3 4
Item4 8
>8 9
.9 :
Empty: ?
)? @
.@ A
ToListAsyncA L
(L M
)M N
;N O
public 
async 
Task 
< 
Item 
> 
GetItemAsync  ,
(, -
Guid- 1
id2 4
)4 5
=>6 8
await 
_collection 
. 
Find "
(" #
item# '
=>( *
item+ /
./ 0
Id0 2
==3 5
id6 8
)8 9
.9 :
FirstOrDefaultAsync: M
(M N
)N O
;O P
public 
async 
Task 
< 
Item 
> 
AddItemAsync  ,
(, -
Item- 1
item2 6
)6 7
{ 	
await 
_collection 
. 
InsertOneAsync ,
(, -
item- 1
)1 2
;2 3
return   
item   
;   
}!! 	
public## 
async## 
Task## 
<## 
Item## 
>## 
UpdateItemAsync##  /
(##/ 0
Item##0 4
item##5 9
)##9 :
{$$ 	
await%% 
_collection%% 
.%% "
FindOneAndReplaceAsync%% 4
(%%4 5
	foundItem%%5 >
=>%%? A
	foundItem%%B K
.%%K L
Id%%L N
==%%O Q
item%%R V
.%%V W
Id%%W Y
,%%Y Z
item%%[ _
)%%_ `
;%%` a
return'' 
item'' 
;'' 
}(( 	
public** 
async** 
Task** 
DeleteItemAsync** )
(**) *
Guid*** .
id**/ 1
)**1 2
=>**3 5
await++ 
_collection++ 
.++ 
DeleteOneAsync++ ,
(++, -
item++- 1
=>++2 4
item++5 9
.++9 :
Id++: <
==++= ?
id++@ B
)++B C
;++C D
},, 
}-- 