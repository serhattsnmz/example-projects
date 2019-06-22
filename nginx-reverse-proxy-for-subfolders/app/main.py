from flask          import Flask
from flask_restful  import Resource, Api

# Create Flask App 
# ----------------

app = Flask(__name__)
api = Api(app)

@api.resource("/api")
class Homepage(Resource):

    def get(self):
        return "Hello world!"

# Run Flask
# ---------

if __name__ == '__main__':
    app.run(host="0.0.0.0", port=8080)