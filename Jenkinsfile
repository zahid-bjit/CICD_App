pipeline {  
 agent any  
 stages {
    stage('Restore') {
      steps {
        bat "\"${tool 'MSBuild'}\" RESTAPITest.sln -t:restore -p:RestorePackagesConfig=true"
      }
    }
    stage('Clean') {
      steps {
        bat "\"${tool 'MSBuild'}\" RESTAPITest.sln -t:clean -p:RestorePackagesConfig=true"
      }
    }
    stage('Build') {
      steps {
        script {
          bat "\"${tool 'MSBuild'}\" RESTAPITest.sln /p:DeployOnBuild=true /p:DeployDefaultTarget=WebPublish /p:WebPublishMethod=FileSystem /p:SkipInvalidConfigurations=true /t:build /p:Configuration=Release /p:Platform=\"Any CPU\" /p:DeleteExistingFiles=True /p:publishUrl=C:\\BuildArtifacts"
        } 
      } 
    }
  	stage('Create Zip Folder') {
      steps {
				 bat '''cd C:\\
				 tar.exe acvf restapi.zip BuildArtifacts'''
      }
    }
    stage('Stop and Start IIS') {
      steps {
          script{
              boolean continuePipeline = true
            try {
              bat 'net stop "w3svc"'
            } catch(Exception e) {
              continuePipeline = false
            }
            
            if(continuePipeline) {
              //do anything

              bat 'net start "w3svc"'
            }
          }
          
      }
 }

  } 
} 
