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
                sh 'docker-compose -f docker-compose.yml -f docker-compose.prod.yml build'                              
            }
        }
        stage('Deploy') {
            steps {
                sh 'docker-compose -f docker-compose.yml -f docker-compose.prod.yml rm -f -s -v'
                sh 'docker-compose -f docker-compose.yml -f docker-compose.prod.yml up -d'                
            }
        }
    }
}
