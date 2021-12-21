function main(x1, x2, n)
    d = (x2 - x1)/n
    x = x1
    arr = {}
    for i = 0, n, 1 do
        table.insert(arr, {x = x, y = f(x)})
        x = x + d
    end
    return arr
end

function f(x)
    return (x - 3.2^(2*x)) / (x^2 + math.log(x^2 + 1)) * (x - math.exp(-x))
end