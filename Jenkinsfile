pipeline {
    agent any
    stages {
        stage('build') {
            steps {
                sh 'dotnet build WebAPI.sln -p:Configuration=release -v:q'
            }
        }
    }
}