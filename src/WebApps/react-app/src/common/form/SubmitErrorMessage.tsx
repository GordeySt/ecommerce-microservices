interface IProps {
    errors: any
}

export const SubmitErrorMessage: React.FC<IProps> = ({ errors }) => {
    return (
        <ul style={{ color: 'red' }}>
            {errors.data && Object.keys(errors.data.errors).length > 0 && (
                <>
                    {Object.values(errors.data.errors)
                        .flat()
                        .map((err: any, i) => (
                            <li key={i}>{err}</li>
                        ))}
                </>
            )}
        </ul>
    )
}
