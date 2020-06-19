namespace Tests.UI.pageObjects
{
    public class LoginTCE
    {
        public String Username { get { return driver.FindElement(By.Id("username")); } }
        public String Password { get { driver.FindElement(By.Id("password")); } }

        public LoginTCE(String username, String password){
            Username.SendKeys(username);
            Password.SendKeys(password);
        }

        public join(){
            driver.FindElement(By.Id("idEntrarLogin")).Click();
        }
    }
}