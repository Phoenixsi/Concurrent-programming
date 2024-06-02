using Logic;

namespace LogicTest
{
    [TestClass]
    public class LoggerTest
    {
        [TestMethod]
        public async Task isEmptyAsync()
        {
            AbstractBallsCollection ballsCollection = new BallsCollection(500, 800);
            int initialBalls = 20;
            ballsCollection.InitBalls(initialBalls);
            ballsCollection.StartTimer();

            await Task.Delay(100);

            ballsCollection.StopTimer();

            string logFilePath = Directory.GetCurrentDirectory() + "\\Balls_Logger.txt";

            // Sprawdź, czy plik istnieje
            Assert.IsTrue(File.Exists(logFilePath), "Plik dziennika istnieje.");

            // Sprawdź, czy plik nie jest pusty
            using (StreamReader sr = new StreamReader(logFilePath))
            {
                string content = sr.ReadToEnd();
                Assert.IsTrue(string.IsNullOrEmpty(content), "Plik dziennika nie jest pusty.");
            }
        }




    }
}
