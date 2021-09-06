module.exports = {
    preset: 'ts-jest',
    testEnvironment: 'node',
    transform: {
        '.(ts|tsx)': 'ts-jest',
    },
    testRegex: '(/tests/.*)\\.(ts|tsx)$',
    testPathIgnorePatterns: ['/node_modules/', '/build/'],
};
