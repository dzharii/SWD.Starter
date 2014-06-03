## Авто-тесты в Visual Studio 2013 Express (Desktop)  
[Оригинал: automated-testing.info](http://automated-testing.info/t/avto-testy-v-visual-studio-2013-express-desktop/3703)  

Visual Studio Express -- это линейка “бесплатной” Visual Studio с ограниченой функциональностью. Основные ограничения были в том, что:

1. Не подержалась установка плагинов для VS, ладно там Resharper нельзя было установить, но ведь и пакетный менеджер Nuget был тоже недоступен, и приходилось подключать новые сборки по-старинке, через поиск файла на диске. 
2. Не поддерживался Ms-Test и тестовые проекты. Можно было использовать NUnit. Это работало, но не особо удобно. Особенно, когда нужно было что-то продебажить. 
3. Были ограничены возможности рефакторинга. Сейчас не скажу точно, но вроде-бы нельзя было переименовать метод средствами IDE, и делать это приходилось поиском и заменой по тексту. 

До версии VS 2013, я устанавливал VS Express… нервно смеялся… и сразу же сносил. 

Возможно наличие альтернатив, таких как  [Xamarin Studio](http://xamarin.com/studio) и [Sharp Develop](http://www.icsharpcode.net), а может быть еще что, все таки заставили Майкрософт расщедрится на добавление фич в VS 2013, которых так не хватало:

1. Плагины до сих пор установить нельзя, но появилась поддержка Nuget. Теперь сборки WebDriver и другие проекты можно устанавливать и обновлять посредством пакетного менеджера. 
2. Добавили поддержку тестовых проектов. Можно запускать и отлаживать тесты внутри IDE. Поддерживается только родной “Ms-Test”
3. Мне вполне хватает возможности “вынести код в отдельный метод”, создать новый метод и переименовать переменную/метод

В целом -- очень доволен. 
Почему написал этот пост? -- Нет, не ради денег :blush: 

Раньше разговаривал с людьми, которые шли путем мучений, используя VS Express 2010/2012 + NUnit. Так вот, мучениям пришел конец! (Только осталось переписать тесты под MsTest :smiley: )
 
[Microsoft Visual Studio Express 2013 для Windows Desktop] (http://www.microsoft.com/ru-ru/download/details.aspx?id=40787)

<hr />

## Автоматическое создание Браузера и инициализация PageObject
[Оригинал: automated-testing.info](http://automated-testing.info/t/zametka-avtomaticheskoe-sozdanie-brauzera-i-iniczializacziya-pageobject/3522)  

### Проблема:

Многих людей, хлебом не корми – дай только пописать лишний код, да и передать лишний вебдрайвер каждому ПейджОбжекту в самый конструктор…

В примере ниже, я покажу, как избежать лишних  явных созданий экземпляра вебдрайвера и лишних инициализаций ```PageFactory.InitElements```

Я понимаю, что многие начали работу с PageObject [по этому примеру с PageFactory]( https://code.google.com/p/selenium/wiki/PageFactory#A_Simple_Example), но ведь это совсем не значит, что этот пример самый оптимальный. Это – просто пример. 

Я не хочу каждый раз инициализировать вебдрайвер. Я не хочу, каждый раз инициализировать страницу при помощи PageFactory. Я просто, хочу писать код… 


### Решение

Для начала, разберемся с надоедливым созданием нового экземпляра WebDriver в каждом тесте. Пусть Вебдрайвер – сам себя создает, когда мне это нужно. 

Вызов Browser.Driver(), вернет либо уже созданный Вебдрайвер, либо создаст его при первом обращении. 

``` csharp
    // Вызови Browser.Driver(), и драйвер  – твой!
    public static class Browser
    {
        private static IWebDriver driver;

        // Автоматически создает новый WebDriver при 
        // первом обращении, либо возвращает уже созданный 
        public static IWebDriver Driver()
        {
            // Если driver равен null (??), то создать новый FirefoxDriver
            driver = driver ?? new FirefoxDriver();
            return driver;
        }

        // Driver капут
        public static void CloseDriver()
        {
            if (driver != null) driver.Quit();
        }

        
        // Эммм… у статических классов – нет деструктора. 
        // Тут создается объект обычного класса Finalizer, 
        // который будет безжалостно уничтожен .NET фреймворком 
        // по завершению теста. А за собой он потянет закрытие 
        // вебдрайвера. 
        static readonly Finalizer finalizer = new Finalizer();
        sealed class Finalizer
        {
            ~Finalizer()
            {
                CloseDriver();
            }
        }
    }
```

Идем дальше.  
Я хочу, чтобы каждая страница *сама себя инициализировала* при помощи PageFactory.InitElements. Тем самым, при создании нового экземпляра страницы, будет возвращаться готовая страница, с которой уже можно работать, а не какой-то полуфабрикат, который еще и на фабрику отправлять нужно.    
Для этого, необходимо создать специальный базовый класс. 

В .NET есть такая особенность: конструкторы без параметров дочерних классов, будут автоматически вызывать, в первую очередь, конструктор базового класса. 
Вы спрашиваете, будет ли это работать и в Java? – Не знаю, попробуйте. 

``` csharp
    // Это базовый класс для всех страниц 
    public abstract class BasePage
    {
        // Полезное свойство. Позволяет писать меньше точек. 
        public IWebDriver Driver { get { return Browser.Driver();  } }

        // Конструктор без параметров дочернего класса, 
        // автоматически вызывает конструктор без параметров базового. 
        // Тут очень важно, что это работает только для конструкторов 
        // ** без параметров **. 
        // На этом свойстве и сыграем.
        public BasePage()
        {
            // На самом деле, this – это будет объект дочернего класса. 
            // PageFactory умеет с ним работать со столь запутанной 
            // схемой. 
            // Убийца – садовник. Извините. 

            PageFactory.InitElements(Driver, this);
        }
    }
```
Пришел черед создать PageObject, который декларирует страницу. В нем есть два действия (метода):  

* Invoke() – прото открывает страницу
* Calcualte() – выполняет действия по вычислению логарифма

``` csharp
    public class LogCalculatorPage : BasePage
    {
        // =================== Элементы страницы =======================
        // 
        [FindsBy(How = How.XPath, Using = @"//input[@name='b']")]
        protected IWebElement txtLogBase { get; set; }

        [FindsBy(How = How.XPath, Using = @"//input[@name='x']")]
        protected IWebElement txtLog { get; set; }

        // Товарищи, эти локаторы были записаны на скорую руку при помощи 
        // SWD Page Recorder 
        // И мне лень было их оптимизировать. Но, это не значит, что это
        // правильный путь. 
        [FindsBy(How = How.XPath, Using = @"//form[@name='calcform1']/table[1]/tbody[1]/tr[5]/td[2]/input[1]")]
        protected IWebElement btnCalculate { get; set; }

        [FindsBy(How = How.XPath, Using = @"//input[@name='y']")]
        protected IWebElement txtResult { get; set; }
        // =================== ~~~~~~~~~~~~~~~~~ =======================


        // Тыщ-тыщ, «вызывает» страницу 
        public void Invoke()
        {
            // Я еще раз напомню, что Driver – унаследован из базового 
            // класса, 
            // А на самом деле, при каждом таком обращении, вызывается 
            // Browser.Driver(). 
            // Но, удобно же, правда?
            Driver.Navigate().GoToUrl(@"http://www.rapidtables.com/calc/math/Log_Calculator.htm");
        }


        // Калькъ!
        public double Calcualte(string logBase, double logValue)
        {
            txtLogBase.SendKeys(logBase);
            
            txtLog.SendKeys(logValue.ToString());

            btnCalculate.Click();

            var rawResult = txtResult.GetAttribute("value");

            return Convert.ToDouble(rawResult);
        }
    }
```  

А вот так выглядит тест:

``` csharp
    static void Main(string[] args)
    {
        // Магия базового класса! При создании объекта, 
        // он уже будет проинициализирован PageFactory
        var calcPage = new LogCalculatorPage();

        calcPage.Invoke();

        var result = calcPage.Calcualte("10", 100);

        Console.WriteLine("Вот такой суровый логарифм: " + result);
    }
```  

Видео работы теста, чтобы вы не говорили, что я обманываю :)


http://www.youtube.com/watch?v=rdJ1k9wahuo&feature=youtu.be

[Весь исходный код – тут](https://gist.github.com/dzhariy/6984074)

<hr />

## Все зависит от проекта… или как выбрать локатор для тестов на WebDriver?
[Оригинал: automated-testing.info](http://automated-testing.info/t/vse-zavisit-ot-proekta-ili-kak-vybrat-lokator-dlya-testov-na-webdriver/3681)  

Зачастую меня немного подтролливают, когда я произношу фразу “все зависит от проекта”. Ну действительно же, зависит. И не только  от проекта, но от фреймвоков, на котором реализуется проект.  

Когда мы выбираем локатор для идентификации элемента страницы, мы же ведь хотим, чтобы он был стабильным, ведь так? А стабильность... это дело относительное... по крайней мере, относительно между различными фреймворками.  

Начнем с клиент-сайда  

### Голый HTML или Bootstrap + JQuery

В этом случае, обычно очень хорошо работает идентификация по HTML id и CSS селекторам.  
Ведь программисты сами обращаются к элементам по id, и учитывая то, что переименование id элемента грозит поиском и заменой по всем файлам... программисты не будут менять его часто, и будут следить, чтобы этот ID был уникальным на странице, как завещает нам великий стандарт HTML.   
Тоже относится и к именам CSS классов. Они, конечно же, могут менятся со временем, но... это болезненно для разработчика, следовательно -- метятся будут не часто.  
Зачастую, для таких страниц, хорошо работает поиск FindElementById или CSS.  
  
Вы еще не уснули?  
  
### Фреймворк Knockout
B вот вам сразу [пример приложения](http://knockoutjs.com/examples/contactsEditor.html).  
Если исследовать типичный элемент накаута -- текстовое поле, то что же мы увидим?  
![](https://github.com/dzharii/SWD.Starter/raw/master/images/knockout-demo01.png) 

```
<input data-bind="value: firstName">
```

Невероятно... согласно здравому смыслу, надо бы написать хотя бы так:  

```
<input type="text" name="firstName" id="firstName"data-bind="value: firstName">
```

Но где же это все? где name? где id? где в конце концов type="text".   
Вы не поверите. Этого всего нет. И это в официальном примере. Вы думаете, что обычный (ленивый) разработчик будет заводить атрибуты name или id специально для вас?  
Ведь разработчику -- они не нужны.   
 
Knockout сам разруливает все привязки. Для этого, ему достаточно строки:   
`data-bind="value: firstName"`  

Следовательно, и локаторы в тестах нужно привязывать к этой строке:   
 
`CSS: input[data-bind="value: firstName"] `  
или   
`CSS: input[data-bind*="firstName"] (менее предсказуемо)`  


### Фреймворк ExtJS
Если с Knockout (а также AngularJS) -- все ясно -- можно использовать их кастомные атрибуты, то ExtJS -- это абсолютно другое дело. В  ExtJS для каждого HTML элемента присваивается свой ID!  
([А вот и примеры](http://dev.sencha.com/deploy/ext-4.0.0/examples/))  
Ура?  

Только... при следующем обновлении страницы, эти ID (все) будут меняться.   
В таком случае, без JavaScript не обойтись. Вот что мне порекомендовали:   
[Ссылка на оригинал](http://habrahabr.ru/post/181660/#comment_6322250)  

>Если в кратце, то наберите в отладчике на указанной вами странице это: 

>`Ext.ComponentManager.each(function (id, item){console.log(id, item.getXTypes(), item.initialConfig)})`

>В консоль выведется весь список компонентов. Из них уже можно вытянуть Id DOM узла, или ссылку на него (Ext.getCmp('id').body.dom — например). Далее этот Id или ссылку (да, да, именно ссылку на DOM) можно передать в Selenium.

>Тут все более менее понятно. Вопрос как найти нужный компонент. 

>Как правило в компонент добавляют какие-то дополнительные данные для реализации прикладного назначения этого компонента. Например имя сущности, идентификатор формы, или адрес поставки данных для хранилища списка. Зная что и где искать можно без труда найти целевой компонент.


Кто может продолжить эту тему боли автоматизатора? :D


## WebDriverWait и PageObject
[Оригинал: automated-testing.info](http://automated-testing.info/t/zametka-c-webdriverwait-i-pageobject/3531)  

### Проблема
Казалось бы, реализация PageObjects и WebDriverWait находятся очень близко друг к другу, прямо в соседних пространствах имен, соответственно:

* OpenQA.Selenium.Support.PageObjects
* OpenQA.Selenium.Support.UI

И я думаю, многие задавались вопросом: а почему WebDriverWait не умеет работать с элементами PageObject «из коробки?»  
Можно, конечно же, «подружить» их между собой… но, тут есть возникает несколько проблем:

1. Во-первых, вам нужно будет самостоятельно отлавливать некоторые исключения, например, StaleElementReferenceException
2. Для создания класса WebDriverWait, в конструктор необходимо отдельно передать экземпляр вебдрайвера или веб-элемента… Но, по сути, внутри элемента PageObject уже есть этот экземпляр… 

Конечно же, магией внедрения зависимостей и реализовывая все интерфейсы на своем пути, можно добиться «правильного и благородного» решения этой проблемы… И, при этом, переопределить 95% кода.   

Раз так, то почему бы не переписать все 100%, и сделать код проще?

### Решение
В C# есть одна замечательная фича – это методы-расширения.   
Если вы еще не знакомы с этой темой, то рекомендую послушать .NET-девочку:  
http://www.youtube.com/watch?v=e5kH3BHoeiQ  

А в нашем примере, специальный метод-расширение `.WaitUntilVisible()`, будет «прилипать» ко всем элемента типа `IWebElement`, и выглядеть это будет так:

``` csharp
var page = new YandexPage();
page.txtSearchBox.WaitUntilVisible().SendKeys("Google");
```
В данном примере `WaitUntilVisible()` – будет вызван для `txtSearchBox`. 

Он подождет в течении секунды появления элемента, а дальше:  
Либо, выбросит исключение, если элемент не найден  
Либо, продолжит выполнение операции SendKeys.   

Сам алгоритм ожидания реализован в классе Wait (Wait.cs).
`Wait.UntilVisible(…)` – принимает элемент страницы, второй – граничное время ожидания. 

Расширения реализованы в классе WebElementExtensions
Которые, просто передают нужные параметры в `Wait.UntilVisible(element, timeOut)`;


https://gist.github.com/dzhariy/7013245 



А сам тест и реализованный PageObject – ниже. 
В этом примере элемент page.txtSearchBox, будет найден сразу же.

`page.txtSearchBox.WaitUntilVisible().SendKeys("Google");`

А вот появления элемента page.txtInvalidSearchBox – код подождет один день, и выбросит TimeOutException (если за это время, элемент не появится на странице)

`page.txtInvalidSearchBox.WaitUntilVisible(TimeSpan.FromDays(1)).SendKeys("Google");`

``` csharp
    [TestFixture]
    public class Class1
    {

        public class YandexPage : CorePage
        {
            [FindsBy(How = How.XPath, Using = @"id(""text"")")]
            public IWebElement txtSearchBox { get; set; }

            [FindsBy(How = How.XPath, Using = @"id(""Trololo-locator"")")]
            public IWebElement txtInvalidSearchBox { get; set; }
        }
        
        [Test]
        public void FirstTest()
        {
            SwdBrowser.Driver.Navigate().GoToUrl(@"http://yandex.ru");
            var page = new YandexPage();

            page.txtSearchBox.WaitUntilVisible().SendKeys("Google");
            page.txtInvalidSearchBox.WaitUntilVisible(TimeSpan.FromDays(1)).SendKeys("Google");

        }
    }
```

Еще, значение можно передавать в миллисекундах, например, следующий код, будет ждать появления элемента ровно 5 сек:  
`page.txtInvalidSearchBox.WaitUntilVisible(5000).SendKeys("Google");`


### Минутка наглого пиара:
Сейчас, я работаю над фреймворком для автоматизированного тестирования на Selenium WebDriver.   Задумка в том, что для начала автоматизации, вам нужно будет лишь скачать его с Github… и просто писать тесты, забыв о львиной доли рутинной работы.   

Все эти, и другие хорошие практики работы с Вебдрайвером, несомненно войдут в фреймворк. 

Сейчас там еще мало чего реализовано и работа кипит. 

Код фреймворка можно найти тут:   

https://github.com/dzhariy/SWD.Starter
 
 
## WebDriver. Метод-расширение для C#, возвращающий значение элемента для input и select тегов
[Оригинал: automated-testing.info](http://automated-testing.info/t/zametka-webdriver-metod-rasshirenie-dlya-c-vozvrashhayushhij-znachenie-elementa-dlya-input-i-select-tegov/3797)  

Как многие уже успели заметить, а особенно в начале работы с Selenium WebDriver, свойство Text у IWebElement, не всегда возвращает ожидаемое значение. 
Например, для многострочного текстового поля:

`<textarea id=”mytext”>Hello</textarea >`
 
Свойство .Text: `driver.FindElement(By.Id(“mytext”)).Text` – вернет ожидаемый результат, т.е. значение «Hello».
А вот для тега, описывающего однострочное текстовое поле:

`<input type=”text” value=”SingleLine Hello”>` – почему-то будет всегда возвращать пустую строку, вместо ожидаемого 
текста ”SingleLine Hello”. 

Тут все дело в том, что стандартный element.Text из IWebElement – всегда возвращает текст, заключенный внутри открытого и закрытого тэга, а в случае с input, нужно прочитать не текст внутри, а атрибут с именем “value”.

Аналогично для выпадающего списка, образуемого элементов select: в начале, нужно найти выбранный элемент `<option>` внутри select, а потом взять его текст. 

Для решения проблемы, я создал следующий метод расширение, который вызывается для веб-элемента следующим образом:
``` csharp
string elementText = driver.FindElement(By.Id(“mytext”)).GetElementText();
```

Реализация:

``` csharp
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Demo.Extensions
{
    public static class MyWebElementExtensions
    {
        /// <summary>
        /// Returns tag-specific element value
        /// </summary>
        public static string GetElementText(this IWebElement element)
        {
            string result = "";
            string tag = element.TagName.ToLower();

            switch (tag)
            {
                case "input":
                    result = element.GetAttribute("value");
                    break;
                case "select":
                    result = new SelectElement(element).SelectedOption.Text;
                    break;
                default:
                    result = element.Text;
                    break;
            }
            return result;
        }
    }
}
```


## Ответ в теме: Слои фреймворка для тестирования Web UI
[Оригинал: automated-testing.info](http://automated-testing.info/t/sloi-frejmvorka-dlya-testirovaniya-web-ui/3559)  

Если смотреть с самого верха, то элементов фреймворка у меня 4:   

![](https://github.com/dzharii/SWD.Starter/raw/master/images/framework_01.png) 


1. **Ядро (Framework Core)** – отвечает за все, что связано с тестами, но там нет ничего, что связано с бизнес-логикой работы приложения: модуль отчетности, методы инициализации вебдрайвера, расширения для вебдрайвера, базовые классы для страниц и тестов. И – глобальную конфигурацию (т.е. чтение данных из глобального файла конфигурации, а также некоторые глобальные свойства в коде)
2. **Database** – обеспечивают выборки из базы данных. У нас есть несколько проектов, но они работают с одной базой данных. По сути, этот модуль обеспечивает доступ к этой базе. 
Здесь описываются методы для получения данных. Их можно вызвать как из теста так и из модели.  Использовать запросы типа «select * from users» – запрещено из тестов и из тестовой модели. Обязательно должен быть создан отдельный метод в Модуле Database. 
3. **Тестовая Модель (TestModel)** – через нее можно производить операции с приложением. Это сборка, где создается абстракции для работы с проектом: вней хранятся PageObject-ы, Классы с данными (DTO; Data Transfer Object). И есть Бизнес-шаги, например, такие как CreateUser(username) и .т.д. Эти бизнес шаги (Steps), например, @joemast, называет – сервисами. 
4. **Тесты** – ну да, как же без них :). Тесты у меня взаимодействуют с приложением через TestModel и  Database. У меня была мысль, сделать взаимодействие только через TestModel. Т.е. скрыть использование Database, но, потом я от этой мысли отказался, так как это порождает еще более запутанный абстракциями код. 

Вместе Tests и TestModel составляют тестовый проект. Такой проект у меня пока один, но предусматривается появление следующих.   

По сути, модуль Tests – это только потребитель функционала, он не предоставляет функциональность другим модулям.   

![](https://github.com/dzharii/SWD.Starter/raw/master/images/framework_02.png) 

Самый важный строительный блок – это PageObjects. И они создаются по достаточно жестким правилам:  

* **Single Responsibility** (из SOLID) – Пейджобжект отвечает только за действия над страницей, которую он описывает. Т.е. на странице нового пользователя, есть методы FillForm и Save, но не вкоем случае не (CreateUser()). В некоторых ситуациях, Педжобжект может взаимодействовать с другими Пейджобжектами и с Бизнес-шагами. Но, делается это на уровне абстракций и API. Пейджобжекты не взаимодействуют с базой данных или веб-элементами других страниц напрямую 
* Все веб-элементы – **private или protected**. Т.е. могут быть использованы только внутри Пейджобжекта и его наследников. Наружу предоставляются лишь методы. 
Интересно, что иногда так руки и чешутся написать в тесте: UserForm.btnSave.Click() – но, нельзя. Нужно создать метод, и поместить эту строку кода а него. Т.е. в тесте можно писать UserFrom.Save().
* **.Invoke()** – в каждом Пейджобжекте есть метод, который открывает страницу по «дефолтному» сценарию. Я рассказывал об этом подходе в докладе [«За пределами PageObject»](http://blog.zhariy.com/2013/02/atdays-pageobject.html)  c 40-й минуты.   
Т.е., чтобы открыть страницу любого уровня вложенности, нужно вызвать, UserPage.Invoke()
* **.GetExpectedControls()** – возвращает «дефолтные» ожидаемые элементы на странице. Внимание, не все, а только самые важные. 

И самое интересное: когда завершено создание нового Пейджобжекта, на него обязательно должен быть написан тест. Тест вызывает .Invoke() страницы, а потом, проверят наличие каждого элемента при помощи .GetExpectedControls().   
Тем самым, создается очень легковесный набор тестов, который с одной стороны, тестирует все самые важные элементы пейджобжекта, а с другой стороны – является хорошим смоук-тестом для каждой страницы приложения. В момент работы тестов, автоматически идет проверка на неожиданные JavaScript-ошибки и крэши сервера (500-е ошибки), их тоже отлавливают смоук тесты.   
В тестах можно использовать пейджобжекты как напрямую, так и работать с бизнес-шагами. Просто, если один и тот же код повторяется очень часто – он выносится как бизнес-шаг. 

>Интересны такие вопросы:  
>1. Используете ли вы сервисный слой?

Да, но от варианта работы только через сервисный слой, я отказался. Это порождает необходимость писать дополнительный код для редко вызываемых действий. Для часто используемых действий, такой код пишется сразу: (User.Create(), User.Open(), User.Delete()) 

> 4.Если используете слои, то как решаете задачу с failure-тестами (как убеждаетесь в правильности реакции системы на тест)

А вот, почему я не запрещаю работать с Пейджобжектами из теста. 

У меня есть несколько тестов на такие варианты, и они взаимодействуют непосредственно с Пейджобжектами. Эти действия, лучше разбросать по пейджобжектам и вызывать из теста, чем добавлять все в один класс бизнес-шагов. 

Это пример реализации теста, который напрямую вызывает методы Пейджобжекта:

``` csharp
var userFormPage = new UserFormPage();
var form = UserFrom.Default;
form.Password = “123”
form.ConfirmPass = “666”
userFormPage.FillForm(form)
userFormPage.VerifyConfirmValidationError(“Confirm and Password fields do not match”)
```


## См. также:

* [За пределами PageObject](http://blog.zhariy.com/2013/02/atdays-pageobject.html)
* [Новые статьи и заметки на форуме AT.info](http://blog.zhariy.com/2013/12/atinfo.html)
* [А как вы проставляете значения полей на формах?](http://automated-testing.info/t/a-kak-vy-prostavlyaete-znacheniya-polej-na-formax/3478)
* [Using WebDriver to automatically check for JavaScript errors on every page](http://watirmelon.com/2012/12/19/using-webdriver-to-automatically-check-for-javascript-errors-on-every-page/)
* [Capturing JavaScript Errors in WebDriver - Even on Page Load!](http://jimevansmusic.blogspot.com/2013/09/capturing-javascript-errors-in.html)


