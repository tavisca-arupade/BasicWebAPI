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

         stage('deploy') {
            steps {
                sh 'dotnet WebAPI/bin/Release/netcoreapp2.2/WebAPI.dll'
            }
        }
    }
    post { 
        always { 
            sh 'deleteDir()'
        }
    }
}