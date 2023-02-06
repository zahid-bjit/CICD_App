pipeline {  
 agent any  
 environment {  
  dotnet = 'C:\\Program Files\\dotnet\\dotnet.exe'  
 }  
 stages {  
  stage('Checkout') {  
   steps {
       git credentialsId: 'f041f544-7a3e-4bd0-8a0e-4c7fde6345b8', url: 'https://github.com/Druvo/CICD_DotNet.git', branch: 'main'
   }  
  }  
 }

 }  
} 