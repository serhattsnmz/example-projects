from flask          import Flask, redirect
from flask_restful  import Resource, Api

# Create Flask App 
# ----------------

app = Flask(__name__)
api = Api(app)

@api.resource("/")
class Homepage(Resource):

    def get(self):
        return redirect("/api")

@api.resource("/api")
class Api(Resource):

    def get(self):
        return "Hello world!"

# Run Flask
# ---------

if __name__ == '__main__':
    app.run(host="0.0.0.0", port=8080)