def getNumberCalories(height,weight,age,sex,lifeStyle,muscleMass):
    h = int(float(height)*100)
    w = float(weight)
    a = int(age)
    if (sex == "M"):
        bmi = 10*w+6.25*h-5*a + 5
    elif (sex == "F"):
        bmi = 10*w+6.25*h-5*a - 161
    else:
        bmi = 10*w+6.25*h-5*a - 100
    bmi = bmi * (1+int(lifeStyle)/10)
    bmi = bmi * (1+int(muscleMass)/20-0.20)
    return bmi



