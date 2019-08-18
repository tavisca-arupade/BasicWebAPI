pipeline {
    
    agent any
    
    stages {
        stage('build') {
            steps {
                powershell 'dotnet build -p:Configuration=release -v:q'
            }
        }

        stage('test') {
            steps {
                powershell 'dotnet test -p:Configiration=release -v:q'
            }
        }

         stage('publish') {
            steps {
                powershell 'dotnet publish -p:Configuration=release -v:q'
            }
        }
        
        stage ('BuildDockerImage')
        {
            steps {
                powershell 'docker build -t aspnetapp -f Dockerfile .'
            }
        }
        
        stage('Login to Docker')
        {
            steps{
                powershell 'docker login -u aditirupade -p boots123'
            }
        }
        
        stage('Tag and Push image to Docker')
        {
            steps{
                    
                    powershell 'docker tag aspnetapp:latest webapi:latest'
                    powershell 'docker push webapi:latest'
            }
        }
        
        stage('Remove local docker image')
        {
            steps{
                    powershell 'docker rmi aspnetapp'
            }
        }
         stage('Pull docker image')
        {
            steps{
                    powershell 'docker pull webapi:latest'
            }
        }
         stage('Run docker image')
        {
            steps{
                    powershell 'docker run -p 8077:10000 --name webapicontainer webapi'
            }
        }
    }
    post
    { 
        always
        { 
            powershell 'docker stop webapicontainer'
            powershell 'docker rm webapicontainer'
            cleanWs()
        }
    }
}