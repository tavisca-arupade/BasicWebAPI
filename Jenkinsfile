pipeline {
    
    agent any

    parameters {
        string (name : 'SolutionName', defaultValue: 'WebAPI.sln',description: '')
        string (name : 'TestProjectName', defaultValue: 'XUnitTestProject1/XUnitTestProject1.csproj',description: '')
        string (name : 'LocalImage', defaultValue: 'aspnetapp',description: '')
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
                powershell """dotnet ${SONARQUBE_PATH} begin /k:"demo" /d:sonar.host.url="${SONARQUBE_URL}" /d:sonar.login="${SONARQUBE_TOKEN}""""
                powershell 'dotnet build'
                powershell 'dotnet ${SONARQUBE_PATH} end /d:sonar.login="${SONARQUBE_TOKEN}"'
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
                powershell 'docker build -t aspnetapp -f Dockerfile .'
            }
        }
        
        stage('Tag and Push image to Docker')
        {
            steps{
                  script{
                    docker.withRegistry('','docker_hub_creds')
                    {
                        
                        powershell 'docker tag aspnetapp:latest aditirupade/webapi:latest'
                        powershell 'docker push aditirupade/webapi:latest'
                    }
                }
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
                    powershell 'docker pull aditirupade/webapi:latest'
            }
        }
         stage('Run docker image')
        {
            steps{
                    powershell 'docker run -p 8077:10000 --name webapicontainer aditirupade/webapi'
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