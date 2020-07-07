using System.Text.RegularExpressions;
namespace Tests.UI.PageObjects
{
    public class ResponsavelPelaApuracaoPO
    {
        private const String IdSelectGrupoUnidadeJurisdicionada = "selectGrupoUnidadeJurisdicionada";
        private const String IdSelectResponsability = "selectObrigacao";
        private const String IdSelectSetor = "selectSetor";

        private String group;
        public String Group { 
            get { return group; }
            set { 
                group = value;
                driver.FindElement (By.XPath ($"//*[text()='{group}']")).Click();
            } 
        }

        private String responsibility;
        public String Responsibility { 
            get { return responsibility; }
            set { 
                responsibility = value;
                driver.FindElement (By.XPath ($"//*[text()='{responsibility}']")).Click();
            } 
        }
        
        private String sector;
        public String Sector { 
            get { return sector; }
            set { 
                sector = value;
                driver.FindElement (By.XPath ($"//*[text()='{sector}']")).Click();
            } 
        }

        public ResponsavelPelaApuracaoPO(String responsibility, String sector, String group) 
        {
            getElementGroup().Click();
            this.Group = group;

            getElementResponsability().Click();
            this.Responsibility = responsibility;

            getElementSector().Click();
            this.Sector = sector;
            
        }
        public getElementGroup() { driver.FindElement(By.Id(IdSelectGrupoUnidadeJurisdicionada)); } 
        public getElementResponsability() { driver.FindElement(By.Id(IdSelectResponsability)); } 
        public getElementSector() { driver.FindElement(By.Id(IdSelectSetor)); } 

        public void register(){
            driver.FindElement(By.Id("idButtonSalvar")).Click();
        }
    }
}