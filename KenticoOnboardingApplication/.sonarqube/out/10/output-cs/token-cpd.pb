Ù
ÖC:\Users\VeronikaL\Desktop\kentico-onboarding-cs\KenticoOnboardingApplication\src\KenticoOnboardingApplication.Api\ApiBootstrapper.cs
	namespace 	(
KenticoOnboardingApplication
 &
.& '
Api' *
{ 
public 

class 
ApiBootstrapper  
:! "
IBootstrapper# 0
{ 
public 
IUnityContainer 
Register '
(' (
IUnityContainer( 7
	container8 A
)A B
=>C E
	container 
. 
RegisterType 
< 
IUrlLocator )
,) *

UrlLocator+ 5
>5 6
(6 7
new7 :'
HierarchicalLifetimeManager; V
(V W
)W X
)X Y
. 
RegisterType 
< 
HttpRequestMessage 0
>0 1
(1 2
new '
HierarchicalLifetimeManager 3
(3 4
)4 5
,5 6
InjectHttpMessage %
(% &
)& '
) 
. 
RegisterType 
< 
IConnectionString /
,/ 0
ConnectionString1 A
>A B
(B C
newC F'
HierarchicalLifetimeManagerG b
(b c
)c d
)d e
;e f
private 
static 
InjectionFactory '
InjectHttpMessage( 9
(9 :
): ;
=>< >
new 
InjectionFactory  
(  !
unityContainer 
=> !
(" #
HttpRequestMessage# 5
)5 6
HttpContext7 B
.B C
CurrentC J
.J K
ItemsK P
[P Q
$strQ h
]h i
) 
; 
} 
} ô
êC:\Users\VeronikaL\Desktop\kentico-onboarding-cs\KenticoOnboardingApplication\src\KenticoOnboardingApplication.Api\App_Start\DependencyConfig.cs
	namespace 	(
KenticoOnboardingApplication
 &
.& '
Api' *
{		 
internal

 
static

 
class

 
DependencyConfig

 *
{ 
public 
static 
void 
Register #
(# $
HttpConfiguration$ 5
config6 <
)< =
{ 	
var 
	container 
= 
new 
UnityContainer  .
(. /
)/ 0
;0 1 
RegisterDependencies  
(  !
	container! *
)* +
;+ ,
config 
. 
DependencyResolver %
=& '
new( +
UnityResolver, 9
(9 :
	container: C
)C D
;D E
} 	
internal 
static 
void  
RegisterDependencies 1
(1 2
IUnityContainer2 A
	containerB K
)K L
=>M O
	container 
. 
Register 
< 
ApiBootstrapper )
>) *
(* +
)+ ,
. 
Register 
< $
RepositoriesBootstrapper 2
>2 3
(3 4
)4 5
. 
Register 
<  
ServicesBootstrapper .
>. /
(/ 0
)0 1
;1 2
private 
static 
IUnityContainer &
Register' /
</ 0
T0 1
>1 2
(2 3
this3 7
IUnityContainer8 G
	containerH Q
)Q R
where 
T 
: 
IBootstrapper #
,# $
new% (
(( )
)) *
=>+ -
new 
T 
( 
) 
. 
Register 
( 
	container &
)& '
;' (
} 
}  
äC:\Users\VeronikaL\Desktop\kentico-onboarding-cs\KenticoOnboardingApplication\src\KenticoOnboardingApplication.Api\App_Start\JsonConfig.cs
	namespace 	(
KenticoOnboardingApplication
 &
.& '
Api' *
{ 
internal 
static 
class 

JsonConfig $
{ 
public 
static 
void 
Register #
(# $
HttpConfiguration$ 5
config6 <
)< =
{		 	
config

 
.

 

Formatters

 
.

 
JsonFormatter

 +
.

+ ,
SerializerSettings

, >
.

> ?
ContractResolver

? O
=

P Q
new 2
&CamelCasePropertyNamesContractResolver :
(: ;
); <
;< =
config 
. 

Formatters 
. 
JsonFormatter +
.+ ,)
UseDataContractJsonSerializer, I
=J K
falseL Q
;Q R
} 	
} 
} Ÿ
ãC:\Users\VeronikaL\Desktop\kentico-onboarding-cs\KenticoOnboardingApplication\src\KenticoOnboardingApplication.Api\App_Start\RouteConfig.cs
	namespace 	(
KenticoOnboardingApplication
 &
.& '
Api' *
{ 
internal 
static 
class 
RouteConfig %
{ 
public		 
static		 
void		 
Register		 #
(		# $
HttpConfiguration		$ 5
config		6 <
)		< =
{

 	
var 
constraintResolver "
=# $
new% (+
DefaultInlineConstraintResolver) H
{ 
ConstraintMap 
= 
{ 
[ 
$str !
]! "
=# $
typeof% +
(+ ,%
ApiVersionRouteConstraint, E
)E F
} 
} 
; 
config 
. "
MapHttpAttributeRoutes )
() *
constraintResolver* <
)< =
;= >
} 	
} 
} Ô
ìC:\Users\VeronikaL\Desktop\kentico-onboarding-cs\KenticoOnboardingApplication\src\KenticoOnboardingApplication.Api\Repositories\ConnectionString.cs
	namespace 	(
KenticoOnboardingApplication
 &
.& '
Api' *
.* +
Repositories+ 7
{ 
internal 
class 
ConnectionString #
:$ %
IConnectionString& 7
{ 
string 
IConnectionString  
.  !
TodoList! )
=>* , 
ConfigurationManager		  
.		  !
ConnectionStrings		! 2
[		2 3
$str		3 =
]		= >
.		> ?
ConnectionString		? O
;		O P
}

 
} àZ
êC:\Users\VeronikaL\Desktop\kentico-onboarding-cs\KenticoOnboardingApplication\src\KenticoOnboardingApplication.Api\Controllers\ListController.cs
	namespace 	(
KenticoOnboardingApplication
 &
.& '
Api' *
.* +
Controllers+ 6
{ 
[ 

ApiVersion 
( 
$str 
) 
] 
[ 
RoutePrefix 
( 
$str 1
)1 2
]2 3
[ 
Route 

]
 
public 

class 
ListController 
:  !
ApiController" /
{ 
private 
readonly 
IListRepository (
_repository) 4
;4 5
private 
readonly 
IUrlLocator $
_urlLocator% 0
;0 1
private 
readonly 
IUpdateItemService +
_updateItemService, >
;> ?
private 
readonly 
ICreateItemService +
_createItemService, >
;> ?
private 
readonly 
IGetItemService (
_getItemService) 8
;8 9
public 
ListController 
( 
IListRepository -

repository. 8
,8 9
IUrlLocator: E
locatorF M
,M N
ICreateItemServiceO a
createItemServiceb s
,s t
IUpdateItemService 
updateItemService 0
,0 1
IGetItemService2 A
getItemServiceB P
)P Q
{ 	
_repository 
= 

repository $
;$ %
_urlLocator 
= 
locator !
;! "
_createItemService 
=  
createItemService! 2
;2 3
_updateItemService   
=    
updateItemService  ! 2
;  2 3
_getItemService!! 
=!! 
getItemService!! ,
;!!, -
}"" 	
public$$ 
async$$ 
Task$$ 
<$$ 
IHttpActionResult$$ +
>$$+ ,
GetAllItemsAsync$$- =
($$= >
)$$> ?
=>$$@ B
Ok%% 
(%% 
await%% 
_repository%%  
.%%  !
GetAllItemsAsync%%! 1
(%%1 2
)%%2 3
)%%3 4
;%%4 5
['' 	
Route''	 
('' 
$str'' 
,'' 
Name''  
=''! "

UrlLocator''# -
.''- .
RouteGet''. 6
)''6 7
]''7 8
public(( 
async(( 
Task(( 
<(( 
IHttpActionResult(( +
>((+ ,
GetItemAsync((- 9
(((9 :
Guid((: >
id((? A
)((A B
{)) 	
ShouldBeIdEmpty** 
(** 
id** 
,** 
false**  %
)**% &
;**& '
if++ 
(++ 
!++ 

ModelState++ 
.++ 
IsValid++ #
)++# $
return,, 

BadRequest,, !
(,,! "
GetErrorMessage,," 1
(,,1 2
),,2 3
),,3 4
;,,4 5
var-- 
result-- 
=-- 
await-- 
_getItemService-- .
.--. /
GetItemAsync--/ ;
(--; <
id--< >
)--> ?
;--? @
if.. 
(.. 
!.. 
result.. 
... 
WasFound..  
)..  !
return// 
NotFound// 
(//  
)//  !
;//! "
return11 
Ok11 
(11 
result11 
.11 
Item11 !
)11! "
;11" #
}22 	
public44 
async44 
Task44 
<44 
IHttpActionResult44 +
>44+ ,
PostItemAsync44- :
(44: ;
[44; <
FromBody44< D
]44D E
Item44F J
value44K P
)44P Q
{55 	$
ValidateTextAndDateTimes66 $
(66$ %
value66% *
)66* +
;66+ ,
ShouldBeIdEmpty77 
(77 
value77 !
.77! "
Id77" $
,77$ %
true77& *
)77* +
;77+ ,
if88 
(88 
!88 

ModelState88 
.88 
IsValid88 #
)88# $
return99 

BadRequest99 !
(99! "
GetErrorMessage99" 1
(991 2
)992 3
)993 4
;994 5
var:: 
uri:: 
=:: 
_urlLocator:: !
.::! "
GetListItemUri::" 0
(::0 1
value::1 6
.::6 7
Id::7 9
)::9 :
;::: ;
var;; 
item;; 
=;; 
await;; 
_createItemService;; /
.;;/ 0
CreateItemAsync;;0 ?
(;;? @
value;;@ E
);;E F
;;;F G
return== 
Created== 
(== 
uri== 
,== 
item==  $
)==$ %
;==% &
}>> 	
[@@ 	
Route@@	 
(@@ 
$str@@ 
)@@ 
]@@ 
publicAA 
asyncAA 
TaskAA 
<AA 
IHttpActionResultAA +
>AA+ ,
PutItemAsyncAA- 9
(AA9 :
GuidAA: >
idAA? A
,AAA B
[AAC D
FromBodyAAD L
]AAL M
ItemAAN R
valueAAS X
)AAX Y
{BB 	$
ValidateTextAndDateTimesCC $
(CC$ %
valueCC% *
)CC* +
;CC+ ,
ShouldBeIdEmptyDD 
(DD 
valueDD !
.DD! "
IdDD" $
,DD$ %
falseDD& +
)DD+ ,
;DD, -
ifEE 
(EE 
!EE 

ModelStateEE 
.EE 
IsValidEE #
)EE# $
returnFF 

BadRequestFF !
(FF! "
GetErrorMessageFF" 1
(FF1 2
)FF2 3
)FF3 4
;FF4 5
varGG 
resultGG 
=GG 
awaitGG 
_updateItemServiceGG 1
.GG1 2
UpdateItemAsyncGG2 A
(GGA B
valueGGB G
)GGG H
;GGH I
ifHH 
(HH 
!HH 
resultHH 
.HH 
WasFoundHH  
)HH  !
returnII 
NotFoundII 
(II  
)II  !
;II! "
returnKK 
OkKK 
(KK 
resultKK 
.KK 
ItemKK !
)KK! "
;KK" #
}LL 	
[NN 	
RouteNN	 
(NN 
$strNN 
)NN 
]NN 
publicOO 
asyncOO 
TaskOO 
<OO 
IHttpActionResultOO +
>OO+ ,
DeleteItemAsyncOO- <
(OO< =
GuidOO= A
idOOB D
)OOD E
{PP 	
ShouldBeIdEmptyQQ 
(QQ 
idQQ 
,QQ 
falseQQ  %
)QQ% &
;QQ& '
ifRR 
(RR 
!RR 

ModelStateRR 
.RR 
IsValidRR #
)RR# $
returnSS 

BadRequestSS !
(SS! "
GetErrorMessageSS" 1
(SS1 2
)SS2 3
)SS3 4
;SS4 5
varTT 
retrievedItemTT 
=TT 
awaitTT  %
_getItemServiceTT& 5
.TT5 6
GetItemAsyncTT6 B
(TTB C
idTTC E
)TTE F
;TTF G
ifUU 
(UU 
!UU 
retrievedItemUU 
.UU 
WasFoundUU '
)UU' (
returnVV 
NotFoundVV 
(VV  
)VV  !
;VV! "
awaitWW 
_repositoryWW 
.WW 
DeleteItemAsyncWW -
(WW- .
idWW. 0
)WW0 1
;WW1 2
returnYY 

StatusCodeYY 
(YY 
HttpStatusCodeYY ,
.YY, -
	NoContentYY- 6
)YY6 7
;YY7 8
}ZZ 	
private\\ 
string\\ 
GetErrorMessage\\ &
(\\& '
)\\' (
=>\\) +
string]] 
.]] 
Join]] 
(]] 
$str]] 
,]] 

ModelState]] )
.]]) *
Values]]* 0
.^^ 

SelectMany^^ 
(^^ 
value^^ !
=>^^" $
value^^% *
.^^* +
Errors^^+ 1
)^^1 2
.__ 
Select__ 
(__ 
error__ 
=>__  
error__! &
.__& '
ErrorMessage__' 3
)__3 4
)__4 5
;__5 6
privateaa 
voidaa $
ValidateTextAndDateTimesaa -
(aa- .
Itemaa. 2
itemaa3 7
)aa7 8
{bb 	
ValidateTextcc 
(cc 
itemcc 
.cc 
Textcc "
)cc" #
;cc# $"
ValidateLastUpdateTimedd "
(dd" #
itemdd# '
.dd' (
LastUpdateTimedd( 6
)dd6 7
;dd7 8 
ValidateCreationTimeee  
(ee  !
itemee! %
.ee% &
CreationTimeee& 2
)ee2 3
;ee3 4
}ff 	
privatehh 
voidhh 
ValidateTexthh !
(hh! "
stringhh" (
texthh) -
)hh- .
{ii 	
ifjj 
(jj 
stringjj 
.jj 
IsNullOrEmptyjj $
(jj$ %
textjj% )
)jj) *
)jj* +

ModelStatekk 
.kk 
AddModelErrorkk (
(kk( )
$strkk) 4
,kk4 5
$strkk6 M
)kkM N
;kkN O
}ll 	
privatenn 
voidnn  
ValidateCreationTimenn )
(nn) *
DateTimenn* 2
timenn3 7
)nn7 8
{oo 	
ifpp 
(pp 
timepp 
!=pp 
DateTimepp  
.pp  !
MinValuepp! )
)pp) *

ModelStateqq 
.qq 
AddModelErrorqq (
(qq( )
$strqq) 4
,qq4 5
$strqq6 S
)qqS T
;qqT U
}rr 	
privatett 
voidtt "
ValidateLastUpdateTimett +
(tt+ ,
DateTimett, 4
timett5 9
)tt9 :
{uu 	
ifvv 
(vv 
timevv 
!=vv 
DateTimevv  
.vv  !
MinValuevv! )
)vv) *

ModelStateww 
.ww 
AddModelErrorww (
(ww( )
$strww) 4
,ww4 5
$strww6 U
)wwU V
;wwV W
}xx 	
privatezz 
voidzz 
ShouldBeIdEmptyzz $
(zz$ %
Guidzz% )
idzz* ,
,zz, -
boolzz. 2
shouldBeEmptyzz3 @
)zz@ A
{{{ 	
if|| 
(|| 
id|| 
==|| 
Guid|| 
.|| 
Empty||  
!=||! #
shouldBeEmpty||$ 1
)||1 2

ModelState}} 
.}} 
AddModelError}} (
(}}( )
$str}}) 4
,}}4 5
shouldBeEmpty}}6 C
?}}D E
$str}}F Y
:}}Z [
$str}}\ q
)}}q r
;}}r s
}~~ 	
} 
}ÄÄ Ó
ÅC:\Users\VeronikaL\Desktop\kentico-onboarding-cs\KenticoOnboardingApplication\src\KenticoOnboardingApplication.Api\Global.asax.cs
	namespace 	(
KenticoOnboardingApplication
 &
.& '
Api' *
{ 
public 

class 
WebApiApplication "
:# $
System% +
.+ ,
Web, /
./ 0
HttpApplication0 ?
{ 
	protected 
void 
Application_Start (
(( )
)) *
{ 	
GlobalConfiguration		 
.		  
	Configure		  )
(		) *
RouteConfig		* 5
.		5 6
Register		6 >
)		> ?
;		? @
GlobalConfiguration

 
.

  
	Configure

  )
(

) *

JsonConfig

* 4
.

4 5
Register

5 =
)

= >
;

> ?
GlobalConfiguration 
.  
	Configure  )
() *
DependencyConfig* :
.: ;
Register; C
)C D
;D E
} 	
} 
} ˙

àC:\Users\VeronikaL\Desktop\kentico-onboarding-cs\KenticoOnboardingApplication\src\KenticoOnboardingApplication.Api\Helpers\UrlLocator.cs
	namespace 	(
KenticoOnboardingApplication
 &
.& '
Api' *
.* +
Helpers+ 2
{ 
public 

class 

UrlLocator 
: 
IUrlLocator )
{ 
private		 
readonly		 
	UrlHelper		 "

_urlHelper		# -
;		- .
public

 
const

 
string

 
RouteGet

 $
=

% &
$str

' ,
;

, -
public 

UrlLocator 
( 
	UrlHelper #
	urlHelper$ -
)- .
=>/ 1

_urlHelper2 <
== >
	urlHelper? H
;H I
public 
Uri 
GetListItemUri !
(! "
Guid" &
id' )
)) *
{ 	
var 
url 
= 

_urlHelper  
.  !
Link! %
(% &
RouteGet& .
,. /
new0 3
{4 5
id5 7
}7 8
)8 9
;9 :
return 
new 
Uri 
( 
url 
) 
;  
} 	
} 
}  
çC:\Users\VeronikaL\Desktop\kentico-onboarding-cs\KenticoOnboardingApplication\src\KenticoOnboardingApplication.Api\Properties\AssemblyInfo.cs
[ 
assembly 	
:	 

AssemblyTitle 
( 
$str ;
); <
]< =
[		 
assembly		 	
:			 

AssemblyDescription		 
(		 
$str		 !
)		! "
]		" #
[

 
assembly

 	
:

	 
!
AssemblyConfiguration

  
(

  !
$str

! #
)

# $
]

$ %
[ 
assembly 	
:	 

AssemblyCompany 
( 
$str 
) 
] 
[ 
assembly 	
:	 

AssemblyProduct 
( 
$str =
)= >
]> ?
[ 
assembly 	
:	 

AssemblyCopyright 
( 
$str 0
)0 1
]1 2
[ 
assembly 	
:	 

AssemblyTrademark 
( 
$str 
)  
]  !
[ 
assembly 	
:	 

AssemblyCulture 
( 
$str 
) 
] 
[ 
assembly 	
:	 


ComVisible 
( 
false 
) 
] 
[ 
assembly 	
:	 

Guid 
( 
$str 6
)6 7
]7 8
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
]##) *
[$$ 
assembly$$ 	
:$$	 

InternalsVisibleTo$$ 
($$ 
$str$$ F
)$$F G
]$$G Há%
çC:\Users\VeronikaL\Desktop\kentico-onboarding-cs\KenticoOnboardingApplication\src\KenticoOnboardingApplication.Api\Resolvers\UnityResolver.cs
	namespace 	(
KenticoOnboardingApplication
 &
.& '
Api' *
.* +
	Resolvers+ 4
{ 
internal 
sealed 
class 
UnityResolver '
:( )
IDependencyResolver* =
{ 
private 
readonly 
IUnityContainer (

_container) 3
;3 4
private 
static 
readonly 
List  $
<$ %
string% +
>+ ,
s_caughtExceptions- ?
=@ A
newB E
ListF J
<J K
stringK Q
>Q R
(R S
)S T
{ 	
nameof 
( %
IHostBufferPolicySelector ,
), -
,- .
nameof 
( #
IHttpControllerSelector *
)* +
,+ ,
nameof 
( $
IHttpControllerActivator +
)+ ,
,, -
nameof 
( 
IHttpActionSelector &
)& '
,' (
nameof 
( 
IHttpActionInvoker %
)% &
,& '
nameof 
( 
IContentNegotiator %
)% &
,& '
nameof 
( 
IExceptionHandler $
)$ %
,% &
nameof 
( !
ModelMetadataProvider (
)( )
,) *
nameof 
(  
IModelValidatorCache '
)' (
}  
 
;   
public"" 
UnityResolver"" 
("" 
IUnityContainer"" ,
	container""- 6
)""6 7
=>""8 :

_container## 
=## 
	container## "
??### %
throw##& +
new##, /!
ArgumentNullException##0 E
(##E F
nameof##F L
(##L M
	container##M V
)##V W
)##W X
;##X Y
public%% 
object%% 

GetService%%  
(%%  !
Type%%! %
serviceType%%& 1
)%%1 2
=>%%3 5
ResolveServiceType&& 
(&& 
(&&  
)&&  !
=>&&" $

_container&&% /
.&&/ 0
Resolve&&0 7
(&&7 8
serviceType&&8 C
)&&C D
,&&D E
null&&F J
)&&J K
;&&K L
public(( 
IEnumerable(( 
<(( 
object(( !
>((! "
GetServices((# .
(((. /
Type((/ 3
serviceType((4 ?
)((? @
=>((A C
ResolveServiceType)) 
()) 
())  
)))  !
=>))" $

_container))% /
.))/ 0

ResolveAll))0 :
()): ;
serviceType)); F
)))F G
,))G H

Enumerable))I S
.))S T
Empty))T Y
<))Y Z
object))Z `
>))` a
())a b
)))b c
)))c d
;))d e
public++ 
IDependencyScope++ 

BeginScope++  *
(++* +
)+++ ,
{,, 	
var-- 
child-- 
=-- 

_container-- "
.--" # 
CreateChildContainer--# 7
(--7 8
)--8 9
;--9 :
return.. 
new.. 
UnityResolver.. $
(..$ %
child..% *
)..* +
;..+ ,
}// 	
public11 
void11 
Dispose11 
(11 
)11 
=>11  

_container11! +
.11+ ,
Dispose11, 3
(113 4
)114 5
;115 6
private33 
static33 
T33 
ResolveServiceType33 +
<33+ ,
T33, -
>33- .
(33. /
Func33/ 3
<333 4
T334 5
>335 6
resolve337 >
,33> ?
T33@ A
returnValue33B M
)33M N
{44 	
try55 
{66 
return77 
resolve77 
(77 
)77  
;77  !
}88 
catch99 
(99 %
ResolutionFailedException99 ,
ex99- /
)99/ 0
when991 5
(996 7
s_caughtExceptions997 I
.99I J
Contains99J R
(99R S
ex99S U
.99U V
TypeRequested99V c
)99c d
)99d e
{:: 
return;; 
returnValue;; "
;;;" #
}<< 
}== 	
}>> 
}?? 