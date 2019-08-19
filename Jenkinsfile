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
                powershell 'dotnet C:/MSBuild/SonarScanner.MSBuild.dll begin /k:"demo" /d:sonar.host.url="http://localhost:9000" /d:sonar.login="89704bb981d3d78d32bc509e119ccc9e40679f00"'
                powershell 'dotnet build'
                powershell 'dotnet C:/MSBuild/SonarScanner.MSBuild.dll end /d:sonar.login="89704bb981d3d78d32bc509e119ccc9e40679f00"'
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