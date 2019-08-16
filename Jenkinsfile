pipeline {
    agent any
    stages {
        stage('build') {
            steps {
                sh 'dotnet build WebAPI.sln -p:Configuration=release -v:q'
            
                sh 'echo deleted workspace'
            }
        }

        stage('test') {
            steps {
                sh 'echo test'
                sh 'dotnet test XUnitTestProject1/XUnitTestProject1.csproj -p:Configiration=release -v:q'
            }
        }

         stage('publish') {
            steps {
                sh 'dotnet publish WebAPI.sln -p:Configuration=release -v:q -o Publish'
            }
        }
    }
    post { 
        always { 
            sh'zip -r artifact.zip WebAPI/Publish/'
            sh 'archiveArtifacts artifacts: artifact.zip'
            sh 'deleteDir()'
        }
    }
}