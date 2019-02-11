using System;
using System.Text;

namespace Guesser {
    internal static class Program {
        private const int Border = 51;
        private const int GoodTries = 10;
        private const int BadTries = 25;
        private static readonly TimeSpan GoodTime = new TimeSpan(0, 0, 30);  // must be less than minute
        private static readonly TimeSpan BadTime = new TimeSpan(0, 3, 0);
        private static readonly string Godmode = "molo4evan";
        
        public static void Main(string[] args) {
            Console.WriteLine("Вечер в хату, братан, черкани кликуху:");
            var nickname = Console.ReadLine() ?? "";
            var answer = nickname.Equals(Godmode)
                ? "Классное имя, где-то я его уже слышал..."
                : "Стремное погоняло, ну да ладно, в семье не без урода...";
            Console.WriteLine("{0}? {1}", nickname, answer);
            Console.WriteLine(
                "Давай, значит, сыграем. Я тебе два сту... В смысле, число от 0 до {0} загадаю, отгадаещь - молодец",
                Border - 1);
            Console.WriteLine("А надоест - \"-q\" пиши");
            var target = new Random().Next(Border);
            if (nickname.Equals(Godmode)) {
                Console.WriteLine("/** О, Великий Создатель, как вы и просили, сообщаю загаданное число: {0} **/", target);
            }
            var tries = 0;
            var queses = new bool[Border];
            for (var i = 0; i < Border; i++) {
                queses[i] = false;
            }
            Console.WriteLine("Ну, поехали, лупи!");
            var start = DateTime.Now;
            while (true) {
                var input = Console.ReadLine() ?? "";
                if (input.Equals("-q")) {
                    Console.WriteLine("Без базара, братан, может в другой раз повезет...");
                    return;
                }
                
                int number;
                if (!int.TryParse(input, out number)) {
                    Console.WriteLine("Ты че, аутичный? Числа пиши, а не дерьмо всякое!");
                    continue;
                }

                if (number < 0 || number >= Border) {
                    Console.WriteLine("Я же сказал, от 0 до {0}, дурик!", Border - 1);
                    continue;
                }

                if (queses[number]) {
                    Console.WriteLine("Ты уже выбирал его, гений...");
                    continue;
                }
                queses[number] = true;

                tries++;
                if (number.Equals(target)) {
                    var end = DateTime.Now;
                    Congratulations(tries, end - start);
                    return;
                }

                if (tries % 4 == 0) {
                    CheerUp();
                }
                Console.WriteLine(number > target ? "Многовато будет..." : "Бери выше!");
            }
        }

        private static void Congratulations(int tries, TimeSpan delta) {
            Console.WriteLine();
            if (tries < GoodTries) {
                Console.WriteLine("От души, братан, ты фартовый!");
                Console.WriteLine("Поехали завтра с Саней в очко зарубимся! С меня 50%!");
                Console.WriteLine("С  {0}-й попытки угадал!", tries);
            }
            else if (tries <= BadTries) {
                Console.WriteLine("Достойно, достойно...");
                Console.WriteLine("Видал я, конечно, типов и пофартовее, но в картишки смело можешь рубануть.");
                Console.WriteLine("{0} попыток - получите, распишитесь!", tries);
            }
            else {
                Console.WriteLine("Мдееееее...");
                Console.WriteLine("От себя бесплатный совет - если в деле вероятность, что все по... плохо, в общем, пойдет,");
                Console.WriteLine("больше 10% - не берись, себе дороже выйдет.");
                Console.WriteLine("{0}-я попытка... Ну, наверное, бывает и хуже (но это не точно).", tries);
            }
            Console.WriteLine("\nНу-ка, а че там по времени...");
            if (delta < GoodTime) {
                Console.WriteLine("А у тебя, брат, похоже, самая быстрая рука на Диком Западе... Девушка-то есть?");
                Console.WriteLine("{0} c... Ну вот как, как?!", delta.Seconds);
            } else if (delta <= BadTime) {
                Console.WriteLine("Добро пожаловать в клуб середнячков, уважаемый.");
                Console.WriteLine("Не совсем пенек, конечно, но и Флешем тебя не назовешь...");
                Console.WriteLine("{0} м {1} с! Раунд!", delta.Minutes, delta.Seconds);
            } else {
                Console.WriteLine("Время прохождения - ... Сколько?!");
                var time = new StringBuilder();
                if (delta.Hours > 0) {
                    time.Append(delta.Hours).Append("ч ").Append(delta.Minutes).Append("м ").Append(delta.Seconds).Append("с");
                } else if (delta.Minutes > 0) {
                    time.Append(delta.Minutes).Append("м ").Append(delta.Seconds).Append("с");
                } else {
                    time.Append(delta.Seconds).Append("сек");
                }
                Console.WriteLine("{0}?! Ты там че, вздремнуть решил?!", time);
                Console.WriteLine("Скорость мышления развивать надо, табуретка ты неторопливая!");
            }
        }

        private static void CheerUp() {
            var index = new Random().Next(Cheers.Length);
            Console.WriteLine(Cheers[index]);
        }

        private static readonly string[] Cheers = {
            "Братан, может остановишься уже? Если б на деньги играли, я бы тебе уже почку вырезал...",
            "Не, я конечно встречал людей, которые не могут в угадайку, но чтоб настолько...",
            "Слушай, может это такой тонкий троллинг, а я не въезжаю?",
            "Ну правда, напрягись уже и роди нужное число!",
            "Так, поездка в Вегас послезавтра отменяется...",
            "ХВАТИТ БЕСТОЛКОВО ДОЛБИТЬ ПО ЦИФЕРКАМ! НАРЯГИ УЖЕ ИНТУИЦИЮ, ИЛИ ЧТО ТАМ У ТЕБЯ!",
            "Познакомься, это Петя. Он отгадывает число с 3-й попытки. Будь как Петя.",
            "Вот ты тут сидишь, циферки тыкаешь... Лучше бы деньги зарабатывал...",
            "А я смотрю, тебя ничему жизнь не учит?",
            "Привет, я пасхалка, если ты меня видишь, значит, считай, уже повезло :3"
        };
    }
}