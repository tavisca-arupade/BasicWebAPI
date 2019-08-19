pipeline {
    
    agent any

    parameters {
        string (name : 'SolutionName', defaultValue: 'WebAPI.sln',description: '')
        string (name : 'TestProjectName', defaultValue: 'XUnitTestProject1/XUnitTestProject1.csproj',description: '')
        string (name : 'LocalImage', defaultValue: 'aspnetapp',description: '')
        string (name : 'RemoteImage', defaultValue: 'webapi',description: '')
        string (name : 'Username', defaultValue: 'aditirupade',description: '')
        string (name : 'ContainerName', defaultValue: 'webapicontainer',description: '')
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

        stage('sonarqube analysis'){
            steps {
                powershell 'dotnet ${env:SONARQUBE_PATH} begin /k:"demo" /d:sonar.host.url="${env:SONARQUBE_URL}" /d:sonar.login="${env:SONARQUBE_TOKEN}"'
                powershell 'dotnet build'
                powershell 'dotnet ${env:SONARQUBE_PATH} end /d:sonar.login="${env:SONARQUBE_TOKEN}"'
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
                powershell 'docker build -t ${env:LocalImage} -f Dockerfile .'
            }
        }
        
        stage('Tag and Push image to Docker')
        {
            steps{
                  script{
                    docker.withRegistry('','docker_hub_creds')
                    {
                        
                        powershell 'docker tag ${env:LocalImage}:latest ${env:Username}/${env:RemoteImage}:latest'
                        powershell 'docker push ${env:Username}/${env:RemoteImage}:latest'
                    }
                }
            }
        }
        
        stage('Remove local docker image')
        {
            steps{
                    powershell 'docker rmi ${env:LocalImage}'
            }
        }
         stage('Pull docker image')
        {
            steps{
                    powershell 'docker pull ${env:Username}/${env:RemoteImage}:latest'
            }
        }
         stage('Run docker image')
        {
            steps{
                    powershell 'docker run -p 8077:10000 --name ${env:ContainerName} ${env:Username}/${env:RemoteImage}'
            }
        }
    }
    post
    { 
        always
        { 
            powershell 'docker stop ${env:ContainerName}'
            powershell 'docker rm ${env:ContainerName}'
            cleanWs()
        }
    }
}