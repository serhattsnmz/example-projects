from flask          import Flask, redirect
from flask_restful  import Resource, Api

from elasticapm.contrib.flask import ElasticAPM

app = Flask(__name__)

app.config['ELASTIC_APM'] = {
  'SERVICE_NAME': 'test-api',
  'SERVER_URL': 'http://apm-server:8200',
  'DEBUG': True
}
apm = ElasticAPM(app)

api = Api(app)

@api.resource("/")
class Homepage(Resource):

    def get(self):
        return redirect("/api1")

@api.resource("/api1")
class Api1(Resource):
    def get(self):
        return { "message" : "This is 1st page of API" }

@api.resource("/api2")
class Api2(Resource):
    def get(self):
        return { "message" : "This is 2st page of API" }

@api.resource("/api3")
class Api3(Resource):
    def get(self):
        return { "message" : "This is 3st page of API" }

if __name__ == '__main__':
    app.run(host="0.0.0.0", port=8080)