pipeline {
    
    agent any

    parameters {
        string (name : 'SolutionName', defaultValue: 'WebAPI.sln',description: '')
        string (name : 'TestProjectName', defaultValue: 'XUnitTestProject1/XUnitTestProject1.csproj',description: '')
        string (name : 'LocalImage', defaultValue: 'aspnetapp',description: '')
        string (name : 'RemoteImage', defaultValue: 'webapi',description: '')
        string (name : 'RemoteRepo', defaultValue: 'aditirupade',description: '')
        string (name : 'DockerContainer', defaultValue: 'webapicontainer',description: '')
    }
    
    stages {
        stage('build') {
            steps {
                powershell 'dotnet build ${SolutionName} -p:Configuration=release -v:q'
            }
        }

        stage('test') {
            steps {
                powershell 'dotnet test ${TestProjectName} -p:Configiration=release -v:q'
            }
        }

         stage('publish') {
            steps {
                powershell 'dotnet publish ${SolutionName} -p:Configuration=release -v:q'
            }
        }
        
        stage ('BuildDockerImage')
        {
            steps {
                powershell 'docker build -t %LocalImage% -f Dockerfile .'
            }
        }
        
        stage('Tag and Push image to Docker')
        {
            steps{
                  script{
                    docker.withRegistry('','docker_hub_creds')
                    {
                        
                        powershell 'docker tag ${LocalImage}:latest ${RemoteRepo}/${RemoteImage}:latest'
                        powershell 'docker push ${RemoteRepo}/${RemoteImage}:latest'
                    }
                    }
            }
        }
        
        stage('Remove local docker image')
        {
            steps{
                    powershell 'docker rmi ${LocalImage}'
            }
        }
         stage('Pull docker image')
        {
            steps{
                    powershell 'docker pull ${RemoteRepo}/${RemoteImage}:latest'
            }
        }
         stage('Run docker image')
        {
            steps{
                    powershell 'docker run -p 8077:10000 --name ${DockerContainer} ${RemoteRepo}/${RemoteImage}'
            }
        }
    }
    post
    { 
        always
        { 
            powershell 'docker stop ${DockerContainer}'
            powershell 'docker rm ${DockerContainer}'
            cleanWs()
        }
    }
}