describe("Orders", () => {
    it(" add a Product", () => {
        cy
            .api({ 
                method: 'POST',
                url: "/products", 
                body: {
                    description: "Produto Teste 01",
                    unitValue: 10.35
                }
            })
            .then((res) => {
                expect(res.status).to.equal(200);
            })
    });

    it("get all Products", () => {
        cy
            .api({ url: "/products"})
            .then((res) => {
                expect(res.status).to.equal(200);
            })
    });
});