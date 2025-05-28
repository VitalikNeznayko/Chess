# ♟️ChessGame
ChessGame — це настільна гра «Шахи», реалізована на C# з використанням Windows Forms. Гравці можуть грати за білих або чорних, робити ходи фігурами, бачити підсвітку доступних ходів і останнього ходу, а також обирати теми оформлення дошки. Гра підтримує базові шахові механіки, такі як перевірка шаху, мату та патової ситуації, із таймерами для кожного гравця.

# 🌟Особливості
- Базові шахові механіки: Переміщення фігур, перевірка шаху, мату та патової ситуації.

- Таймери: Налаштування часу гри (1, 5 або 10 хвилин на гравця).

- Теми оформлення: Вибір стилів дошки (класична, синя, зелена).

- Інтерактивний інтерфейс:

  - Підсвітка доступних ходів (фіолетовий для порожніх клітинок, зелений для захоплення).

  - Підсвітка останнього ходу (жовтий) та короля в шаху (червоний).

- Історія ігор: Перегляд попередніх партій.

# 🔢Рядки коду

- Кількість рядків в коді
  
![lines](Screenshots/numLines.jpg)

# 📷Скріншоти
  - Головне меню
    
  ![MainMenu](Screenshots/MainMenu.jpg)
  
  - Меню налаштування
    
  ![MainMenu](Screenshots/Settings.jpg)
  
  - Меню історії ігор
    
  ![HistoryOfGames](Screenshots/HistoryOfGame.jpg)
  - Ігрова дошка
    
  ![Board](Screenshots/Board.jpg)
  
  - Активність на ігровій дошці

  ![BoardActive](Screenshots/BoardActive.jpg)

# 📋Вимоги
- .NET Framework 4.7.2 або новіший
- Visual Studio 2019 або новіший (для розробки)

# 🔧Встановлення
1. Клонуйте репозиторій:
   ```
   git clone https://github.com/VitalikNeznayko/Chess.git
   ```
3. Відкрийте рішення (`ChessGame.sln`) у Visual Studio.
4. Зберіть і запустіть проєкт.

# 🎮 Використання
1. **Запустіть гру**:
   - Відкрийте додаток і з головного меню натисніть "Почати гру" або "Налаштування".
2. **Налаштуйте гру**:
   - Виберіть час на партію (1, 5 або 10 хвилин) і тему дошки (класична, синя, зелена) у меню налаштувань.
3. **Грайте**:
   - Клікніть на свою фігуру, щоб вибрати її (доступно лише для фігур поточного гравця: білі або чорні).
   - Клікніть на клітинку призначення:
     - 🟣 **Фіолетова підсвітка**: Хід на порожню клітинку.
     - 🟢 **Зелена підсвітка**: Захоплення ворожої фігури.
     - 🔴 **Червона підсвітка**: Ваш король у шаху.
   - Жовта підсвітка показує останній хід.
4. **Слідкуйте за грою**:
   - Таймери у верхній частині екрана показують залишок часу для кожного гравця.
   - Гра автоматично виявляє шах, мат, пат або закінчення часу.
5. **Завершення гри**:
   - Після мату, пату чи закінчення часу з’явиться повідомлення про результат.
   - Виберіть «Грати знову», щоб почати нову партію, або натисніть «Back to Menu», щоб повернутися до головного меню.

# 🧬Programming Principles
1. **DRY** (Don't Repeat Yourself)
    - Клас [PieceFactory](/ChessGame/classes/PieceFactory.cs) відповідає принципу DRY, централізуючи створення шахових фігур. Замість дублювання логіки для створення фігур, таких як [King](/ChessGame/classes/Pieces/King.cs), [Queen](/ChessGame/classes/Pieces/Queen.cs) тощо, використовується фабричний метод ([CreatePiece](/ChessGame/classes/PieceFactory.cs#L28-L53)) для створення екземплярів фігур залежно від типу та кольору.
    - Метод [UpdateTimerLabels](/ChessGame/GameForm.cs#L108-L112) централізує логіку відображення часу для обох гравців, уникаючи дублювання форматування рядків часу.
    - Метод [SwitchPanel](/ChessGame/MainForm.cs#L101-L107) узагальнює логіку перемикання між панелями, що використовується для переходу між головним меню, налаштуваннями та історією.
2. **KISS** (Keep It Simple, Stupid)
    - Метод [GetClickedPosition](/ChessGame/classes/BoardPanel.cs#L227-L232) просто обчислює позицію клітинки на основі координат кліка миші, використовуючи чітку формулу: `clickedRow = (y - CoordinateOffset) / cellSize`. Це простий і зрозумілий підхід.
    - Метод [GetMinutesFromSelectedTime](/ChessGame/GameForm.cs#L57-L63) використовує простий `switch` для визначення хвилин на основі вибраного режиму гри, що легко зрозуміти.
3. **SOLID**
    1. **S — Single Responsibility Principle**
    - Клас [GameLogger](/ChessGame/classes/GameLogger.cs) відповідає за логування результатів гри та має єдину відповідальність — запис і читання логів.
    - Клас [GameForm](/ChessGame/GameForm.cs) відповідає за управління інтерфейсом гри, включаючи таймери та шахову дошку.
    2. **O — Open/Closed Principle**
    - Метод [CreatePiece](/ChessGame/classes/PieceFactory.cs#L28-L53) кидає виняток, якщо не знайдено зображення.
    - Метод [UpdateTimeForCurrentPlayer](/ChessGame/GameForm.cs#L89-L104) завершує гру, якщо час вийшов.
    3. **L — Liskov Substitution Principle**
    - Абстрактний клас [Piece](/ChessGame/classes/Piece.cs) дозволяє використовувати будь-яку фігуру через метод [IsValidMove](/ChessGame/classes/Piece.cs#L26).
4. **YAGNI (You Aren't Gonna Need It)**
    - Принцип YAGNI полягає в тому, щоб не додавати функціональність до того, як вона стане потрібною. Код не містить зайвих методів або полів, які не використовуються.
5. **Fail Fast**
    - Метод [CreatePiece](/ChessGame/classes/PieceFactory.cs#L32-L35) кидає виняток, якщо не знайдено зображення для певного типу фігури та кольору.
  
# 🎨Design Patterns
У проєкті використано кілька патернів проєктування для забезпечення гнучкості та масштабованості:
1. **Singleton**:
  - Мета: Забезпечує єдиний екземпляр класу [ChessGame](/ChessGame/classes/chess/ChessGame.cs) для централізованого керування станом гри (дошка, хід, позиції королів).
  - Використання: Властивість [Instance](/ChessGame/classes/chess/ChessGame.cs#L44-L55) гарантує, що всі компоненти (наприклад, [BoardPanel](/ChessGame/classes/BoardPanel.cs), [GameForm](/ChessGame/GameForm.cs)) працюють з одним об’єктом гри.

2. **Observer**:
  - Мета: Дозволяє автоматично оновлювати UI при зміні стану гри (хід, шах, завершення гри).
  - Використання: Подія [GameStateChanged](/ChessGame/classes/chess/ChessGame.cs#L22) у [ChessGame](/ChessGame/classes/chess/ChessGame.cs) сповіщає [BoardPanel](/ChessGame/classes/BoardPanel.cs) (для перемальовування дошки) і [GameForm](/ChessGame/GameForm.cs) (для оновлення таймерів чи повідомлень).

3. **Factory** (PieceFactory.cs):
  - Мета: Спрощує створення об’єктів фігур ([King](/ChessGame/classes/Pieces/King.cs), [Queen](/ChessGame/classes/Pieces/Queen.cs) тощо) з уніфікованим інтерфейсом.
  - Використання: [PieceFactory](/ChessGame/classes/PieceFactory.cs) створює фігури з відповідними іконками та властивостями, що використовується в [InitializeBoard()](/ChessGame/classes/chess/ChessGame.cs#L57-L67).

# 🛠Refactoring Techniques
Під час розробки було використано кілька технік рефакторингу для покращення структури та читабельності коду:
- **Extract Method**: Довгі методи, такі як [Board_Paint](/ChessGame/classes/BoardPanel.cs#L85-L92) і [BoardPanel_MouseClick](/ChessGame/classes/BoardPanel.cs#L203-L223), розбито на менші функції (наприклад, [DrawCell](/ChessGame/classes/BoardPanel.cs#L141-L144), [HandleKingMove](/ChessGame/classes/BoardPanel.cs#L283-L305)), що спрощує розуміння та тестування.
- **Encapsulate Field**: Приватні поля, такі як [selectedPiece](/ChessGame/classes/BoardPanel.cs#L12) у [BoardPanel](/ChessGame/classes/BoardPanel.cs), доступні лише через методи, що зменшує прямий доступ до даних.
- **Rename Method/Variable**: Назви методів і змінних зроблено описовими, наприклад, TryMakeMove замість загальних назв, щоб чітко відобразити їх призначення.
- **Simplify Conditionals**: Складні умовні конструкції в [BoardPanel_MouseClick](/ChessGame/classes/BoardPanel.cs#L203-L223) спрощено шляхом винесення логіки в окремі методи, як-от [IsValidMove](/ChessGame/classes/BoardPanel.cs#L273-L279).
- **Introduce Explaining Variable**: Уведено проміжні змінні, наприклад, /ChessGame/classes/BoardPanel.cs#L99 у [DrawBoard](/ChessGame/classes/BoardPanel.cs#L96-L139), щоб зробити умовні вирази більш читабельними.

# 🤝Контриб’ютинг
Ваші внески вітаються! Щоб долучитися:
1. Форкніть репозиторій.
2. Створіть гілку для нової функції (`git checkout -b your-feature`).
3. Зафіксуйте зміни (`git commit -m "Add your feature"`).
4. Надішліть гілку (`git push origin your-feature`).
5. Створіть Pull Request.
   
Будь ласка, дотримуйтесь стилю коду, якщо це можливо.
