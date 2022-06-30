<img width="1316" alt="Screenshot 2022-06-30 at 13 14 50" src="https://user-images.githubusercontent.com/13501355/176653069-685a5014-6cc9-4a6e-9518-b8ab2f16591d.png">

Сегодня мы обсудим подходы к организации проекта Unity и познакомимся с некоторыми архитектурными паттернами, которые помогут создать упорядоченную, легко поддерживаемую структуру кода.

<img width="1204" alt="Screenshot 2022-06-30 at 13 15 19" src="https://user-images.githubusercontent.com/13501355/176653148-a735938b-b757-468f-a5d6-8d0c30842f20.png">

При разработке любого программного проекта следует придерживаться правилам SOLID.
Принципы SOLID C# представляют собой набор утверждений, которые описывают архитектуру программных продуктов. То есть, следуя им можно разработать стабильно работающее и масштабируемое приложение, которое будет удобно поддерживать.

Принцип единственной ответственности (S)
Согласно этому принципу класс разрабатывается с одной четко определенной целью. По сути своей, любой класс – это инструмент. Соответственно, все элементы класса должны быть направлены на решение одной задачи. Разрабатывая класс для всего и сразу, мы рискуем получить кучу проблем при дальнейшей его поддержке.

Принцип открытости/закрытости (O)
Данный принцип гласит о том, что разрабатываемые классы должны быть открыты для расширений, но закрыты для изменений. То есть, если класс был реализован и протестирован, мы не должны его изменять. Разумеется, изменения могут вноситься, при наличии веских для того причины. При этом, любой класс может бессчетное число раз расширяться при помощи механизма наследования.

Принцип подстановки Лисков (L)
Этот принцип говорит о том, что мы должны иметь возможность работать с любым производным от родительского классом так же, как с родительским. Иными словами, дочерние классы не должны нарушать определения родительского класса и его поведение.

Принцип разделения интерфейсов (I)
Согласно этому принципу клиенты не должны принудительно внедрять интерфейсы, которые ими не используются.
Другими словами, интерфейсы не должны содержать методы, которые не будут реализованы всеми классами которые его наследуют

Принцип инверсии зависимостей (D)
Согласно принципу инверсии зависимостей, классы высокого уровня не должны зависеть от низкоуровневых, а абстракции не должны зависеть от деталей. Как правило, высокоуровневые классы отвечают за бизнес-правила / логику программного продукта. Низкоуровневые классы реализуют более мелкие операции: взаимодействие с данными, передача сообщений в систему и т.п.
К примеру, если в игре есть менеджер сетевых запросов, он не должен быть явно зависим от менеджеров, которые его используют. К примеру LeaderBoardManager не должен контролировать логику работы менеджера сетевых запросов.

Если применить эти правила к Unity, то мы получим следующий список правил:
один «предмет» = один префаб;
логика для одного «предмета» = один класс MonoBehavior;
приложение = сцена со взаимосвязанными префабами;

Этих принципов было бы достаточно для простого проекта. Но по мере роста проекта, количество правил по организации кода и файлов будут расширяться. 
Давайте рассмотрим наиболее популярные проблемы проектирования приложений и обсудим способы их решения.

<img width="1200" alt="Screenshot 2022-06-30 at 13 16 01" src="https://user-images.githubusercontent.com/13501355/176653311-d0ca91de-ee77-490e-aa57-c7f96f348559.png">

На скриншоте показан компонент Slice, прикрепленный к префабу Slice, отображаемый в окне Inspector:
Как видим, у компонента есть четыре параметра. При этом на первый взгляд сложно понять за что отвечают его свойства и как их следует модифицировать.
Это может вызвать сложности с пониманием у разработчиков и гейм-дизайнеров. 

Для добавления дополнительной информации к свойствам MonoBehaviour вы можете использовать атрибуты Tooltip и Header.
Атрибут Tooltip позволяет создать всплывающую подсказку для атрибута. Эта подсказка должно детально описывать возможные значения для свойства и то для чего он будет использоваться в коде.
Атрибут Header позволяет группировать параметры по общему признаку.
Некоторые из параметров могут быть общими для нескольких объектов. Мы можем хранить эти параметры в коде используя статический класс или константы. Но это повлияет на удобство редактирования этих параметров. К примеру, художник или гейм-дизайнер не сможет модифицировать эти значения. Мы так же могли бы дублировать эти значения, либо вынести их в отдельный компонент и ссылаться на него. Но правильным путём решить эту проблему является использование ScriptableObject. Вы можете создать ScriptableObject, который будет содержать все необходимые параметры, которые можно настраивать напрямую из редактора. А для доступа к этим параметрам из компонента использовать ссылку на ScriptableObject.

К примеру, конфигурацию для позиционирования кусочков можно преобразовать в ScriptableObject. Впоследствии нам может потребоваться добавить больше конфигураций, ScriptableObject отлично подходит для этого.

https://unity.com/how-to/architect-game-code-scriptable-objects

<img width="1203" alt="Screenshot 2022-06-30 at 13 16 41" src="https://user-images.githubusercontent.com/13501355/176653427-4fb5de39-51d9-4541-8e33-f19c86d6e79b.png">

Если бы это была настоящая игра, то мы бы наблюдали постепенный рост объема классов MonoBehavior. Давайте рассмотрим, как разделить их на основе принципа одиночной ответственности, подразумевающего, что один класс должен отвечать за один элемент. Если применить его правильно, то у вас должна быть возможность дать короткий ответ на вопросы вроде «За что отвечает конкретный класс?» или «За что он не отвечает?» Благодаря этому ваши коллеги смогут понять, что делает отдельно взятый класс. Это принцип, который можно применить к кодовой базе любого масштаба. Зачастую встречаются реализации, где игровой менеджер управляет всеми элементами игры. Он отслеживает попадания при выстреле, следит за счетом и обрабатывает нажатие на кнопки. Такой подход создает путанницу и ухудшает читаемость кода.
Правило, которым мы воспользуемся для решения этой проблемы будет - делегирование логики дочерним компонентам. Основная идея заключается в том, что многие объекты, которые так или иначе содержат какую-то личную логику отображения должны иметь свой MonoBehaviour, который ее обрабатывает.
К примеру, мы можем разбить класс GameBoard, который отвечает за работу всей игры на четыре класса - Player, Score, Score и GameBoard. Каждый из классов будет отвечать за обработку и отображение лишь той части логики, которая относится к объекту к которому он привязан. А GameBoard будет координировать работу этих классов.
В такой реализации каждый класс является самодостаточным и отвечает строго за объект, к которому он подключен.
Score - координирует отображение и подсчет очков;
ScorePlace - отвечает за размещение слайсов, подсчет очков и проверку SlicePlace на степень готовности;
Slice - Содержит методы для изменения внешнего вида и обработку анимаций;

<img width="1202" alt="Screenshot 2022-06-30 at 13 17 22" src="https://user-images.githubusercontent.com/13501355/176653564-36d95a83-e542-430b-a273-3777289ee98b.png">

Можно перенести MonoBehavior в обычные классы C#, но в чем преимущество такого способа?
Обычные классы C# располагают большими возможностями, чем собственные объекты Unity, с точки зрения создания компактных и совместимых друг с другом структур. Кроме того, обычный код C# можно использовать с другим кодом .NET за пределами Unity. Это может быть полезно, например для передачи клиент-серверных структур, сериализации и тд.
С другой стороны, если использовать обычные классы C#, то редактор не воспримет объекты, не сможет нативно отобразить их в инспекторе.
Этим методом лучше разделять логику по зонам ответственности. Если взглянуть на наш пример, то мы вынесли простую физическую модель в класс C#, который назвали StorageProfileService. Единственная его задача —  обработка и хранение игрового прогресса и прочих данных профиля игрока. Этот класс оперирует не скриптами MonoBehaviour, а обычными классами C#.
Таким образом, мы можем использовать  классы GameSimulation, Ball и Player вне Unity, к примеру на сервере .Net для обработки и управления игровой логикой.
А так же мы еще больше разделили классы, согласно их ответственности.

<img width="1203" alt="Screenshot 2022-06-30 at 13 18 12" src="https://user-images.githubusercontent.com/13501355/176653724-fd82e887-7ee8-4007-b52f-d7ad679350e9.png">

Наследование и интерфейсы помогают нам избежать повторов кода и организовать удобную, поддерживаемую структуру кода.
Давайте рассмотрим несколько примеров использования наследования и интерфейсов.
На первом примере SliceBase является базовым классом, которые реализует методы PostMoveAction, MoveToSlicePlace и определяет свойства kind и type. (показать на слайде где на скриншоте Slice и BombSlice)
Классы Slice и BombSlice наследуют его , а так же наследуют эти методы и свойства. Теперь мы можем быть уверены, что независимо от типа Slice, он обязательно будет поддерживать перемещение и содержать метод для выполнения действий после перемещения.

Вторая ситуация, когда использования наследования и интерфейсов может пригодиться - это игровые сервисы и менеджеры, которые могут иметь несколько реализаций.
Предположим в начале разработки вашей игры, вы не хотите поддерживать сохранение игрового прогресса на стороне сервера, но в перспективе планируете реализовать эту возможность.
Правильным подходом в этой ситуации будет - создание интерфейса IStorageService, который будет содержать все необходимые методы для работы с сохранениями.
После чего создать класс PrefsStorage  и унаследовать интерфейс IStorageService.
В игровом коде вы должны использовать ссылки на IStorageService, а не использовать PrefsStorage напрямую.
Это позволит вам на определенном этапе создать класс ServerStorage, который так же как и PrefsStorage унаследует класс IStorageService и реализует методы Get, Set.
Далее вам не составит труда заменить PrefsStorage на ServerStorage изменением пары строк кода.

<img width="1201" alt="Screenshot 2022-06-30 at 13 18 27" src="https://user-images.githubusercontent.com/13501355/176653783-a44aa3bf-5330-4a10-90d0-f42f459e3986.png">

Давайте взглянем на примеры архитектуры более крупных проектов. Если воспользоваться примером Slices, то начав внедрять специализированные классы в код, такие как Slice, SlicePlace, GameBoard и так далее, мы можем построить следующую иерархию: 
Класс Gameboard, который содержит игровую логику, подсчет очков и обрабатывает действия пользователя;
Класс Score, обрабатывает и выводит текущий игровой счет;
Класс SlicePlace, который контролирует работу места для перемещения слайса и содержит в себе ссылки на слайсы;
Класс Slice, который представляет собой одну игровую единицу и содержит всю информацию об этом слайсе.
Этот подход работает отлично до тех пор, пока в игре не появляются спецэффекты, аудиосопровождение или анимации. Если в игру добавятся анимации перемещения, звуки, различные темы для слайсов, то каждый из классов так же будет реализовывать методы для работы с UI. Таким образом классы уже будут содержать не только игровую логику и техническую информацию, но отвечать за отображение текущего элемента. 
Поход, когда игровая логика и отображение обрабатываются в одном классе подходит для маленьких проектов. Но по мере роста проекта, код станет сложно поддерживать и игровая логика будет плохо понятна, так как она будет перемешана с логикой отображения. Постарайтесь разделить логику и систему представления. Попробуйте сделать так, чтобы кодовая база могла работать в двух режимах — только логика и логика с представлением данных.
Для решения такого рода задач существую архитектурные шаблоны, которые мы обсудим чуть позже.

<img width="1203" alt="Screenshot 2022-06-30 at 13 18 43" src="https://user-images.githubusercontent.com/13501355/176653840-892d7b83-6c90-474a-8deb-0401f3b9ec2e.png">

Иногда вполне допустим класс, содержащий только данные, без включения в него всей логики и операций с этими данными.
Кроме того, бывают полезны классы, которые не имеют никаких данных, но содержат функции, цель которых — выполнять операции с объектами или структурами.
К примеру, давайте рассмотрим класс DateUtils. Он содержит константы, которые можно использовать для форматирования и вывода даты в различных форматах.
Метод YesterdayBeginTime предоставляет возможность конвертировать текущую дату в формат Java
А метод ToJavaMilliseconds конвертирует дату в формат Java с учетом временной зоны
Класс TaskUtils расширяет системный класс Task и добавляет возможность запускать асинхронные операции в синхронном контексте и выполнять колбеки после завершения Task. Расширять системные классы стоит в тех случаях, если встроенных возможностей недостаточно.

<img width="1200" alt="Screenshot 2022-06-30 at 13 18 57" src="https://user-images.githubusercontent.com/13501355/176653885-7bf9b1ce-ea5a-4850-96a5-979a7a11639c.png">

Если следовать методу разделения представления и логики, то перед нами откроется возможность быстро и независимо от UI тестировать игровую логику
К примеру, вы можете создать игровую симуляцию и запустить тестирование уровней.
В этот момент игровая симуляция самостоятельно пройдет все уровни, уничтожит всех врагов. После чего проверит количество очков и наград.
Это является самой надежной страховкой от непредвиденного поведения игровой симуляции и позволяет быстро выявить ошибку в игровой логике.

<img width="1200" alt="Screenshot 2022-06-30 at 13 19 14" src="https://user-images.githubusercontent.com/13501355/176653936-16f61829-ac3a-4065-bdfd-af7ff2c15b54.png">

Для построения правильной архитектуры игры необходимо пользоваться паттернами.
В сегодняшних примерах я задействую паттерны Factory и Observable.
Паттерн Observable следует применять для отслеживания состояния переменных, которые могут измениться во время игры, а их изменение должно вызвать незамедлительную реакцию остального когда. К примеру, при разыве соединения с сервером мы должны немедленно остановить игру и вывести сообщение. А при возобновлении соединения продолжить игру.
В игре Slices паттерн Observable  использован для отслеживания изменений игрового счета.
Этот паттерн может быть полезен в тех случаях, когда данные могут измениться извне и вам необходимо отслеживать и реагировать на эти изменения.
Игровой счет может быть обновлен через дебаг-меню, либо через GameBoard.
Мы могли бы отслеживать эти изменения создав событие. Но что случится если таких отслеживаемых данных станет больше, система событий станет очень запутана и не очевидна.
В данной реализации, переменная, которая хранит количество очков является Observable, а ScoreManager отслеживает изменения этой переменной, после чего меняет значение, отображаемое на экране.

<img width="1202" alt="Screenshot 2022-06-30 at 13 19 26" src="https://user-images.githubusercontent.com/13501355/176653978-69fd5637-0d64-4da0-8939-8e41e039cf37.png">

Также в игре использован паттерн Factory для создания слайсов.
Этот паттерн реализован в двух вариация.
В первом - вы можете запросить инстанс одного и того же объекта.
Он используется для создания посадочных площадок для слайсов. Вы можете создать до 4х инстансов этого объекта, каждый из них после создания будет автоматически перемещен согласно конфигурации.

<img width="1204" alt="Screenshot 2022-06-30 at 13 19 42" src="https://user-images.githubusercontent.com/13501355/176654022-1e0ce60b-01e8-46a3-854c-4b236a97fc99.png">

Вторая реализация позволяет создавать инстансы различных объектов в зависимости от переданного типа.
Этот скрипт используется для создания новых слайсов. Передавая тип слайса мы можем генерировать и инстанциировать префаб для этого слайса. Сейчас поддерживается  обычный слайс и слайс - бомба.

<img width="1203" alt="Screenshot 2022-06-30 at 13 19 54" src="https://user-images.githubusercontent.com/13501355/176654061-709b7e9f-4563-4589-a2f8-af3544c27244.png">

https://itnan.ru/post.php?c=1&p=481618

https://ru.hexlet.io/blog/posts/chto-takoe-mvc-rasskazyvaem-prostymi-slovami

MVC — подход к проектированию игр, который предполагает выделение кода в блоки модель, представление и контроллер. Контроллер обрабатывает информацию и отдает ее на отрисовку. Модель представляет виртуальную модель отображаемых данных, необходимую для отображения того или иного игрового объекта. Представление отображает информацию, согласно приказам контроллера.

Рассмотрим этот паттерн на примере из жизни. В роли контроллера у нас выступает повар, в роли модели - меню, а в роли представления - готовое блюдо.
Каждый пользователь игры - посетитель ресторана и может оценить на вкус, цвет и качество лишь готовое блюдо.
Повар берет все необходимые ингредиенты, согласно рецепту и готовит их, чтобы блюдо получилось вкусным.
Если во после приготовления блюдо получилось недостаточно соленым, контроллер может это понять и добавить немного соли или перца, по вкусу.

Сразу хотелось бы обратить внимание что текущая реализация не является единственным вариантом. Вы можете найти множество реализаций этого паттерна для Unity.

<img width="1202" alt="Screenshot 2022-06-30 at 13 20 05" src="https://user-images.githubusercontent.com/13501355/176654105-6a9602df-aa52-456e-bc26-9aa20d9e287f.png">

В MVC View применяются для визуализации некоторой части модели в виде пользовательского интерфейса.
Давайте рассмотрим реализацию View на примере SliceView. 
Эта View отвечает за отображение и перемещение слайса и содержит вспомогательные методы, которые могут быть вызваны из контроллера чтобы изменить представление.
К примеру, каждый слайс имеет тип. Этот тип определяет положение слайса в UI. Он должен быть повернут на определенное количество градусов в зависимости от типа.
SliceController передает тип слайса, а SliceView реализует поворот слайса.
Каждый отображаемый объект, который содержит логику работы и отображения должен иметь собственный View и контроллер.

<img width="1200" alt="Screenshot 2022-06-30 at 13 20 21" src="https://user-images.githubusercontent.com/13501355/176654152-d1b3b7db-e814-461d-94e2-495f8038618e.png">

Model - это обычный C# класс, который содержит и представляет данные.
Модели могут быть использованы в контроллерах или сервисах, которые обрабатывают или модифицируют эту информацию.
К примеру, на первом скриншоте представлена ProfileDataModel. Это модель используется в ProfileService, который в свою очередь сериализует и сохраняет эту модель в хранилище. Таким образом информация о состоянии пользователя и его данные представлены этой моделью.
Второй вариант реализации - это модель для контроллера SliceController. Эта модель хранит информацию о слайсе, его тип, разновидность и количество очков, которое он принесет.
Общую структуру моделей проекта можно увидеть на последнем скриншоте.

<img width="1201" alt="Screenshot 2022-06-30 at 13 20 33" src="https://user-images.githubusercontent.com/13501355/176654203-dbe218cc-1918-4029-a660-9957351c7aad.png">

Controller представляет центральный компонент MVC, который обеспечивает связь между пользователем и приложением, представлением и хранилищем данных. Он содержит логику обработки запроса пользователя. Контроллер получает вводимые пользователем данные и обрабатывает их. К примеру, LoadingController занимается отслеживанием прогресса загрузки игры и обновляет LoadingView для обновления полоски прогресса загрузк игры.
ScoreController - обрабатывает и отображает текущий игровой прогресс. Он взаимодействует с IProfileService для сохранения информации о прогрессе в профиль игрока.

<img width="1202" alt="Screenshot 2022-06-30 at 13 20 46" src="https://user-images.githubusercontent.com/13501355/176654231-31f7bd32-cff2-4870-9f8f-314802831270.png">

Вы можете использовать подход “сервисы” для создания вспомогательных классов.
Эти классы всегда должны быть обычными C# классами, а не MonoBehaviour.
Они могут иметь Model. К примеру ProfileService имеет модель, в которой хранит информацию о пользователе. Эта модель сериализуется и сохраняется на диск.
Сервисы запускаются на этапе загрузки игры. Обычно, на экране загрузки игры запускаются все сервисы и в асинхронных методах инициализации они загружают информацию с сервера или диска, производят авторизацию пользователя, получают список игр с сервера и тд.
Сервисы могут быть зависеть друг от друга. К примеру, LoginService использует NetworkService для выполнения сетевых запросов.
Порой, сервисы имеют более 10 зависимостей и регулировать эти зависимости вручную становится сложно. Для решения этих проблем существует Dependency Injection. О ней мы поговорим позже.
Как обсуждалось ранее, мы должны получать доступ к сервису лишь через интерфейс, это позволяет создать поддерживаемый и легко обновляемый сервис.
На следующем слайде представлен пример сервиса для хранения игровой статистики в PlayerPrefs.

<img width="1204" alt="Screenshot 2022-06-30 at 13 20 55" src="https://user-images.githubusercontent.com/13501355/176654257-81877a90-3e8d-4f9d-bfc8-ea2c6995d7ab.png">

Как вы видите, этот класс реализует интерфейс IProfileService и его методы.
Таким образом, если мы захотим реализовать хранение данных, в бинарном формате, мы можем написать еще одну реализацию IProfileService и использовать ее в коде.
А для смены хранения информации в PlayerPrefs или бинарном формате нам будет достаточно изменить всего одну строчку кода.

<img width="1202" alt="Screenshot 2022-06-30 at 13 21 07" src="https://user-images.githubusercontent.com/13501355/176654289-51de502d-9e79-434d-a0f8-4f5d9945cd12.png">

Если постараться визуализировать взаимодействие контроллеров, сервисов, View и моделей, то это будет выглядеть следующим образом

<img width="1202" alt="Screenshot 2022-06-30 at 13 21 21" src="https://user-images.githubusercontent.com/13501355/176654341-470c9b8b-5422-4a97-936d-619c9617a285.png">

https://blog.extrawurst.org/gamedev/unity/programming/2020/11/11/scalable-unity-architecture.html

Любая игра состоит из множества систем: UI, звук, графика, ввод и тд и тп. Эти системы неизбежно взаимодействуют
В онлайн шутере игрок А убил игрока Б. Нужно вывести сообщение об этом в игровой лог.
В экономической стратегии завершилось строительство здания. Нужно проиграть звук уведомления и показать отметку на карте.
Игрок нажал на клавишу быстрого сохранения. Обработчик ввода должен передать сообщение об этом в систему сохранения.

Порой, отслеживать подобные вещи в одном месте становится затруднительно. А наладить взаимодействие между классами для прямой передачи события - невозможно.
Для решения этой проблемы существует система событий, также называемая EventBus. Она позволяет отправить событие из любого скрипта
И гарантировать, что в подписавшемся на это событие скрипте будет вызван определенный колбэк.
В проекте, который я вам предоставлю есть нестандартная реализация менеджера событий. 
Эти события задействованы для обработки и обновления счета игрока. Это очень полезно, если вы меняете счет игрока при помощи дебаг-меню.

<img width="941" alt="Screenshot 2022-06-30 at 13 21 49" src="https://user-images.githubusercontent.com/13501355/176654449-0fcd8aef-7a0e-4c42-8af2-893e6f147f9e.png">
