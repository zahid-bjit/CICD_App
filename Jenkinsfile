pipeline {
  agent any
 environment {  
  dotnet = 'C:\\Program Files\\dotnet\\dotnet.exe'  
 }  
  stages {
    stage('Clean'){
    steps{
        bat "dotnet clean ${WORKSPACE}\\CICD_App\\RESTAPITest.sln"
     }
   }
 stage('Build') {  
   steps {  
    bat 'dotnet build ${WORKSPACE}\\CICD_App\\RESTAPITest.sln --configuration Release' 
   }  
  }  
  
  stage("Release"){
      steps {
      bat 'dotnet build  ${WORKSPACE}\\CICD_App\\RESTAPITest.sln /p:PublishProfile="${WORKSPACE}\\CICD_App\\RESTAPITest\\Properties\\PublishProfiles\\FolderProfile.pubxml" /p:Platform="Any CPU" /p:DeployOnBuild=true /m'
    }
  }
  
  stage('Deploy') {
    steps {

    bat 'net start "w3svc"'
    // Stop IIS
    bat 'net stop "w3svc"'
    
    // Deploy package to IIS
    //bat "\"C:\\Program Files (x86)\\IIS\\Microsoft Web Deploy V3\\msdeploy.exe\" ${WORKSPACE}\\CICD_App\\RESTAPITest.sln /p:DeployOnBuild=true /p:DeployDefaultTarget=WebPublish /p:WebPublishMethod=FileSystem /p:SkipInvalidConfigurations=true /t:build /p:Configuration=Release /p:Platform=\"Any CPU\" /p:DeleteExistingFiles=True /p:publishUrl=c:\\inetpub\\wwwroot"
    bat 'xcopy /s ${WORKSPACE}\\RESTAPITest\\bin\\app.publish C:\\inetpub\\wwwroot'

    // Start IIS again
    bat 'net start "w3svc"'
    }
 }
  }
} 