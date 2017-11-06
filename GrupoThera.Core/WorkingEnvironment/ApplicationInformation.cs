using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GrupoThera.Core.WorkingEnvironment
{
    public class ApplicationInformation
    {
        #region Fields

        private static ApplicationInformation instance;

        private readonly Assembly assem = Assembly.GetEntryAssembly();

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationInformation"/> class.
        /// </summary>
        public ApplicationInformation()
        {
            if (this.assem == null)
            {
                // Uses executing dll instead application
                this.assem = Assembly.GetExecutingAssembly();
            }
            this.ApplicationName = this.GetApplicationNameFromAssembly();
            this.ApplicationTitle = this.GetApplicationTitleFromAssembly();
            this.BuildDateTime = File.GetCreationTime(this.assem.Location);
            this.BuildNumber = this.GetBuildNumberFromAssembly();
            this.CodeName = this.GetCodeNameFromAssembly();
            this.Company = this.GetApplicationCompanyFromAssembly();
            this.Copyright = this.GetApplicationCopyrightFromAssembly();
            this.Description = this.GetApplicationDescriptionFromAssembly();
            this.VersionString = this.GetVersionStringFromAssembly();

            this.ProductName = string.Format("{0} - {1}", this.ApplicationName, this.CodeName);
            this.ApplicationEnvironment = "Environment";
            this.Version = new Version(string.Format("{0}.{1}", this.VersionString, this.BuildNumber));
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <value>
        /// The instance.
        /// </value>
        public static ApplicationInformation Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ApplicationInformation();
                }
                return instance;
            }
        }

        /// <summary>
        /// Gets the application environment.
        /// </summary>
        /// <value>
        /// The application environment.
        /// </value>
        public string ApplicationEnvironment
        {
            get; private set;
        }

        /// <summary>
        /// Gets the name of the application.
        /// </summary>
        public string ApplicationName
        {
            get; private set;
        }

        /// <summary>
        /// Gets the application title.
        /// </summary>
        public string ApplicationTitle
        {
            get; private set;
        }

        /// <summary>
        /// Gets the build date time.
        /// </summary>
        /// <value>
        /// The build date time.
        /// </value>
        public DateTime BuildDateTime
        {
            get; private set;
        }

        /// <summary>
        /// Gets the build number.
        /// </summary>
        public string BuildNumber
        {
            get; private set;
        }

        /// <summary>
        /// Gets the name of the code.
        /// </summary>
        /// <value>
        /// The name of the code.
        /// </value>
        public string CodeName
        {
            get; private set;
        }

        /// <summary>
        /// Gets the company.
        /// </summary>
        /// <value>
        /// The company.
        /// </value>
        public string Company
        {
            get; private set;
        }

        /// <summary>
        /// Gets the copyright.
        /// </summary>
        public string Copyright
        {
            get; private set;
        }

        /// <summary>
        /// Gets the description.
        /// </summary>
        public string Description
        {
            get; private set;
        }

        /// <summary>
        /// Gets the name of the product.
        /// </summary>
        /// <value>
        /// The name of the product.
        /// </value>
        public string ProductName
        {
            get; private set;
        }

        /// <summary>
        /// Gets the version string.
        /// </summary>
        public string VersionString
        {
            get; private set;
        }

        /// <summary>
        /// Gets the version.
        /// </summary>
        /// <value>
        /// The version.
        /// </value>
        public Version Version
        {
            get; private set;
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Gets the application company from assembly.
        /// </summary>
        /// <returns>The application company attribute. If there is no application company in
        /// the assembly info, method returns the default company.</returns>
        private string GetApplicationCompanyFromAssembly()
        {
            var copyrightAttribute = this.GetAttributeFromAssembly<AssemblyCompanyAttribute>();
            return copyrightAttribute != null ? copyrightAttribute.Company : string.Empty;
        }

        /// <summary>
        /// Gets the application copyright from the assembly info.
        /// </summary>
        /// <returns>The application copyright attribute. If there is no application copyright in
        /// the assembly info, method returns the default copyright.</returns>
        private string GetApplicationCopyrightFromAssembly()
        {
            var copyrightAttribute = this.GetAttributeFromAssembly<AssemblyCopyrightAttribute>();
            return copyrightAttribute != null ? copyrightAttribute.Copyright : string.Empty;
        }

        /// <summary>
        /// Gets the application description from the assembly info.
        /// </summary>
        /// <returns>The application description attribute. If there is no application description in
        /// the assembly info, method returns the default description.</returns>
        private string GetApplicationDescriptionFromAssembly()
        {
            var descriptionAttribute = this.GetAttributeFromAssembly<AssemblyDescriptionAttribute>();
            return descriptionAttribute != null ? descriptionAttribute.Description : string.Empty;
        }

        /// <summary>
        /// Gets the application name from the assembly info.
        /// </summary>
        /// <returns>The application name attribute. If there is no application name in
        /// the assembly info, method returns the default application name.</returns>
        private string GetApplicationNameFromAssembly()
        {
            var assemblyProduct = this.GetAttributeFromAssembly<AssemblyProductAttribute>();
            var appName = assemblyProduct != null ? assemblyProduct.Product : string.Empty;
            return string.IsNullOrEmpty(appName) ? this.assem.GetName().Name : appName;
        }

        /// <summary>
        /// Gets the application title from the assembly info.
        /// </summary>
        /// <returns>The application title attribute. If there is no application title in
        /// the assembly info, method returns the default application name.</returns>
        private string GetApplicationTitleFromAssembly()
        {
            var assemblyTitle = this.GetAttributeFromAssembly<AssemblyTitleAttribute>();
            var appTitle = assemblyTitle != null ? assemblyTitle.Title : string.Empty;
            return string.IsNullOrEmpty(appTitle) ? this.assem.GetName().Name : appTitle;
        }

        /// <summary>
        /// Gets the assembly version string from assembly.
        /// </summary>
        /// <returns>The assembly version string.</returns>
        private string GetAssemblyVersionStringFromAssembly()
        {
            var assemblyVersion = this.GetAttributeFromAssembly<AssemblyFileVersionAttribute>();
            return assemblyVersion != null ? assemblyVersion.Version : string.Empty;
        }

        /// <summary>
        /// Gets the version string from assembly.
        /// </summary>
        /// <returns>The version string</returns>
        private string GetVersionStringFromAssembly()
        {
            var version = this.GetAssemblyVersionStringFromAssembly().Split('.');
            return string.Join(".", version.Take(version.Count() - 1));
        }

        /// <summary>
        /// Gets the build number from assembly.
        /// </summary>
        /// <returns>The build number.</returns>
        private string GetBuildNumberFromAssembly()
        {
            var version = this.GetAssemblyVersionStringFromAssembly().Split('.');
            return string.Join(".", version.Skip(version.Count() - 1));
        }

        /// <summary>
        /// Gets the code name from assembly.
        /// </summary>
        /// <returns>The code name.</returns>
        private string GetCodeNameFromAssembly()
        {
            var codeName = this.GetAttributeFromAssembly<AssemblyInformationalVersionAttribute>();
            return codeName != null ? codeName.InformationalVersion : string.Empty;
        }

        /// <summary>
        /// Gets the attribute from assembly.
        /// </summary>
        /// <typeparam name="T">A property type of an assembly.</typeparam>
        /// <returns>An attribute of the given type.</returns>
        private T GetAttributeFromAssembly<T>()
        {
            object[] attributes = this.assem.GetCustomAttributes(typeof(T), false);
            if (attributes.Length == 0)
            {
                return default(T);
            }
            return (T)attributes[0];
        }

        #endregion Methods
    }
}
