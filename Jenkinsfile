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
				 tar.exe acvf BuildArtifacts.zip BuildArtifacts'''
      }
    }
  } 
} 
