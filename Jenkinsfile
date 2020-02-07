pipeline {
    agent any

    options {
        skipDefaultCheckout true
    }

    stages {
        stage('Checkout') {
            steps {
                checkout scm
            }
        }
        stage('Build') {
            steps {
                dir ('Production') {
                	sh 'docker-compose build'
                }                                
            }
        }
        stage('Deploy') {
            steps {
                dir ('Production') {
                	sh 'docker-compose rm -f -s -v'
                    sh 'docker-compose up -d'
                }
                
            }
        }
    }
}
